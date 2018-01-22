namespace XamarinFormsDemo.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using GalaSoft.MvvmLight.Views;
    using Xamarin.Forms;

    public class ExtendedNavigationService : INavigationService
    {
        /// <summary>
        ///     The pages by key.
        /// </summary>
        private readonly Dictionary<string, Type> pagesByKey = new Dictionary<string, Type>();

        /// <summary>
        ///     The navigation.
        /// </summary>
        private NavigationPage navigation;

        /// <summary>
        ///     Gets the current page key.
        /// </summary>
        public string CurrentPageKey
        {
            get
            {
                lock(this.pagesByKey)
                {
                    if(this.navigation.CurrentPage == null)
                    {
                        return null;
                    }

                    var pageType = this.navigation.CurrentPage.GetType();

                    return this.pagesByKey.ContainsValue(pageType) ? this.pagesByKey.First(p => p.Value == pageType).Key : null;
                }
            }
        }

        /// <summary>
        ///     The go back.
        /// </summary>
        public void GoBack()
        {
            this.navigation.PopAsync();
        }

        /// <summary>
        ///     The navigate to.
        /// </summary>
        /// <param name="pageKey">
        ///     The page key.
        /// </param>
        public void NavigateTo(string pageKey)
        {
            this.NavigateTo(pageKey, null, null);
        }

        /// <summary>
        /// Instructs the navigation service to display a new page
        /// corresponding to the given key, and passes a parameter
        /// to the new page.
        /// Depending on the platforms, the navigation service might
        /// have to be Configure with a key/page list.
        /// </summary>
        /// <param name="pageKey">The key corresponding to the page
        /// that should be displayed.</param>
        /// <param name="parameter">The parameter that should be passed
        /// to the new page.</param>
        public void NavigateTo(string pageKey, object parameter)
        {
            this.NavigateTo(pageKey, parameter, null);
        }

        /// <summary>
        ///     The configure.
        /// </summary>
        /// <param name="pageKey">
        ///     The page key.
        /// </param>
        /// <param name="pageType">
        ///     The page type.
        /// </param>
        public void Configure(string pageKey, Type pageType)
        {
            lock(this.pagesByKey)
            {
                if(this.pagesByKey.ContainsKey(pageKey))
                {
                    this.pagesByKey[pageKey] = pageType;
                }
                else
                {
                    this.pagesByKey.Add(pageKey, pageType);
                }
            }
        }

        /// <summary>
        ///     The initialize.
        /// </summary>
        /// <param name="nav">
        ///     The navigation.
        /// </param>
        public void Initialize(NavigationPage nav)
        {
            this.navigation = nav;
            this.navigation.Popped += this.Navigation_Popped;
            //Vacía el binding al hacer Pop y no se quedan las páginas con los bindings anteriores y el nuevo.   
            //Provocaba problemas.
        }

        /// <summary>
        ///     The navigate root to.
        /// </summary>
        /// <param name="pageKey">
        ///     The page key.
        /// </param>
        public void NavigateRootTo(string pageKey)
        {
            if(this.CurrentPageKey != pageKey)
                this.NavigateTo(pageKey);

            this.EmptyNavigationStack();
        }

        /// <summary>
        ///     The navigate root to.
        /// </summary>
        /// <param name="pageKey">
        ///     The page key.
        /// </param>
        /// <param name="parameter">
        ///     The parameter.
        /// </param>
        public void NavigateRootTo(string pageKey, object parameter)
        {
            if(this.CurrentPageKey != pageKey)
                this.NavigateTo(pageKey, parameter);

            this.EmptyNavigationStack();
        }

        /// <summary>
        ///     The navigate root to.
        /// </summary>
        /// <param name="pageKey">
        ///     The page key.
        /// </param>
        /// <param name="parameter1">
        ///     The parameter.
        /// </param>
        /// <param name="parameter2">
        ///     The parameter.
        /// </param>
        public void NavigateRootTo(string pageKey, object parameter1, object parameter2)
        {
            if(this.CurrentPageKey != pageKey)
                this.NavigateTo(pageKey, parameter1, parameter2);

            this.EmptyNavigationStack();
        }

        /// <summary>
        ///     The navigate to.
        /// </summary>
        /// <param name="pageKey">
        ///     The page key.
        /// </param>
        /// <param name="parameter1">
        ///     The parameter 1.
        /// </param>
        /// <param name="parameter2">
        ///     The parameter 2.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        public void NavigateTo(string pageKey, object parameter1, object parameter2)
        {
            lock(this.pagesByKey)
            {
                if(this.pagesByKey.ContainsKey(pageKey))
                {
                    var type = this.pagesByKey[pageKey];

                    var existingPages = this.navigation.Navigation.NavigationStack.ToList();
                    var lastPage = this.navigation.Navigation.NavigationStack[existingPages.Count - 1];
                    if(lastPage.GetType() == type)
                    {
                        this.navigation.PopAsync();
                        return;
                    }

                    ConstructorInfo constructor;
                    object[] parameters;

                    if(parameter1 == null && parameter2 == null)
                    {
                        constructor = type.GetTypeInfo().DeclaredConstructors.FirstOrDefault(c => !c.GetParameters().Any());

                        parameters = new object[]
                        {
                        };
                    }
                    else if(parameter1 != null && parameter2 == null)
                    {
                        constructor = type.GetTypeInfo().DeclaredConstructors.FirstOrDefault
                            (
                             c =>
                             {
                                 var p = c.GetParameters();
                                 return p.Count() == 1 && p[0].ParameterType == parameter1.GetType();
                             });

                        parameters = new[]
                        {
                            parameter1
                        };
                    }
                    else
                    {
                        constructor = type.GetTypeInfo().DeclaredConstructors.FirstOrDefault
                            (
                             c =>
                             {
                                 var p = c.GetParameters();
                                 return parameter1 != null && (p.Count() == 2 && p[0].ParameterType == parameter1.GetType() && p[1].ParameterType == parameter2.GetType());
                             });

                        parameters = new[]
                        {
                            parameter1,
                            parameter2
                        };
                    }

                    if(constructor == null)
                    {
                        throw new InvalidOperationException("No suitable constructor found for page " + pageKey);
                    }

                    var page = constructor.Invoke(parameters) as Page;
                    this.navigation.PushAsync(page);
                }
                else
                {
                    throw new ArgumentException($"No such page: {pageKey}. Did you forget to call NavigationService.Configure?", nameof(pageKey));
                }
            }
        }

        /// <summary>
        ///     The empty navigation stack.
        /// </summary>
        private void EmptyNavigationStack()
        {
            try
            {
                var existingPages = this.navigation.Navigation.NavigationStack.ToList();
                var lastPage = existingPages.Count - 2;
                if (lastPage > 0)
                {
                    for (var i = lastPage; i >= 0; i--)
                    {
                        this.navigation.Navigation.RemovePage(existingPages[i]);
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        /// <summary>
        ///     Navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Navigation_Popped(object sender, NavigationEventArgs e)
        {
            e.Page.BindingContext = null;
        }
    }
}
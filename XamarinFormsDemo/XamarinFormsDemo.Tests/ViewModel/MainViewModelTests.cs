using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Moq;
using XFDemo.ViewModel;
using Xunit;

namespace XFDemo.Tests.ViewModel
{
    public class MainViewModelTests
    {
        private readonly FirstViewModel vm;
        private readonly Mock<IMessenger> messengerService;
        private readonly Mock<INavigationService> navigationService;
        private readonly Mock<IDialogService> dialogService;

        public MainViewModelTests()
        {
            this.messengerService = new Mock<IMessenger>();
            this.navigationService = new Mock<INavigationService>();
            this.dialogService = new Mock<IDialogService>();

            this.vm = new FirstViewModel(messengerService.Object, navigationService.Object, dialogService.Object);
        }

        [Fact]
        public void GoToCarouselPageCommand_Should_NavigateToCarouselPage()
        {
            // Arrange

            // Act
            this.vm.GoToCarouselPageCommand.Execute(null);

            // Assert
            this.navigationService.Verify(v => v.NavigateTo(It.IsAny<string>()), Times.Once);
        }
    }
}
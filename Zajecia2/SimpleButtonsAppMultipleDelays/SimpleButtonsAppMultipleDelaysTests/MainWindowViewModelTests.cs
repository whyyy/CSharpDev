using FluentAssertions;
using NSubstitute;
using SimpleButtonsApp;

namespace SimpleButtonsAppMultipleDelaysTests
{
    public class MainWindowViewModelTests
    {
        private MainWindowViewModel sut;
        private string infoLabel;

        [SetUp]
        public void Setup()
        {
            sut = Substitute.For<MainWindowViewModel>();
        }

        [Test]
        public async Task Should_update_info_label_property_when_start_multiple_delays()
        {
            //given
            sut.InfoLabel = "Test";

            //when
            await sut.StartMultipleCommand.ExecuteAsync(CancellationToken.None);

            //then
            sut.InfoLabel.Should().Be("Start multiple button was clicked");
        }
    }
}
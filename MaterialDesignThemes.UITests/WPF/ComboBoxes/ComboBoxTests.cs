using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using XamlTest;
using Xunit;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests.WPF.ComboBoxes
{
    public class ComboBoxTests : TestBase
    {
        public ComboBoxTests(ITestOutputHelper output)
            : base(output)
        {
        }

        [Fact]
        [Description("Pull Request 2192")]
        public async Task OnComboBoxHelperTextFontSize_ChangesHelperTextFontSize()
        {
            await using var recorder = new TestRecorder(App);

            var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <ComboBox
        materialDesign:HintAssist.HelperTextFontSize=""20""/>
</StackPanel>");
            var comboBox = await stackPanel.GetElement<ComboBox>("/ComboBox");
            var helpTextBlock = await comboBox.GetElement<TextBlock>("/Grid/Canvas/TextBlock");

            double fontSize = await helpTextBlock.GetFontSize();

            Assert.Equal(20, fontSize);
            recorder.Success();
        }

        [Fact]
        [Description("Pull Request 2192")]
        public async Task OnFilledComboBoxHelperTextFontSize_ChangesHelperTextFontSize()
        {
            await using var recorder = new TestRecorder(App);

            var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <ComboBox
        Style=""{StaticResource MaterialDesignFilledComboBox}""
        materialDesign:HintAssist.HelperTextFontSize=""20""/>
</StackPanel>");
            var comboBox = await stackPanel.GetElement<ComboBox>("/ComboBox");
            var helpTextBlock = await comboBox.GetElement<TextBlock>("/Grid/Canvas/TextBlock");

            double fontSize = await helpTextBlock.GetFontSize();

            Assert.Equal(20, fontSize);
            recorder.Success();
        }

        [Fact]
        [Description("Issue 2340")]
        public async Task OnEditableComboBoxInMenu_ItCanBeExpandedWhenClicked()
        {
            await using var recorder = new TestRecorder(App);

            var stackPanel = await LoadXaml(@"
<StackPanel VerticalAlignment=""Center"" HorizontalAlignment=""Center"">
    <Menu IsMainMenu=""True"">
        <MenuItem Header=""Doesn't Work on Single Click"" x:Name=""TopMenuItem"">
            <MenuItem.Items>
                <ComboBox Style=""{StaticResource MaterialDesignComboBox}"" x:Name=""ComboBox"" IsEditable=""True"">
                    <ComboBoxItem Content=""One"" IsSelected=""True"" />
                    <ComboBoxItem Content=""Two"" />
                    <ComboBoxItem Content=""Three"" />
                </ComboBox>
            </MenuItem.Items>
        </MenuItem>
    </Menu>
<ComboBox Style=""{StaticResource MaterialDesignComboBox}"">
    <ComboBoxItem Content=""One"" IsSelected=""True"" />
    <ComboBoxItem Content=""Two"" />
    <ComboBoxItem Content=""Three"" />
</ComboBox>
<ComboBox Style=""{StaticResource MaterialDesignComboBox}"" IsEditable=""True"">
    <ComboBoxItem Content=""One"" IsSelected=""True"" />
    <ComboBoxItem Content=""Two"" />
    <ComboBoxItem Content=""Three"" />
</ComboBox>
</StackPanel>");
            var menuItem = await stackPanel.GetElement<MenuItem>("TopMenuItem");
            //Leave time for the menu animations
            //await Task.Delay(300);
            //await menuItem.Click();
            ////wait for child menu to animate in
            //await Task.Delay(300);
            //await Wait.For(async () => await menuItem.GetIsSubmenuOpen());
            //
            //var comboBox = await menuItem.GetElement<System.Windows.Controls.ComboBox>("ComboBox");
            //
            //await Task.Delay(5000);
            //
            //
            //await comboBox.Click();

            //NB: Need to wait a bit to ensure the menu does not trigger a close
            //await Task.Delay(5000);
            //
            //await Wait.For(async () => await comboBox.GetIsDropDownOpen());
            //Assert.True(await menuItem.GetIsSubmenuOpen(), "Sub menu is not open");

            recorder.Success();
        }

    }
}

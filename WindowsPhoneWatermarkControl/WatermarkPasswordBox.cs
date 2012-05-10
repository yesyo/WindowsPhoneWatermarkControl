using System.Windows;
using System.Windows.Controls;

namespace WindowsPhoneWatermarkControl
{
    public class WatermarkPasswordBox : TextBox
    {
        ContentControl WatermarkContent;
        private PasswordBox PasswordContent;

        public object WatermarkText
        {
            get { return base.GetValue(WatermarkProperty) as object; }
            set { base.SetValue(WatermarkProperty, value); }
        }

        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("WatermarkText", typeof(object), typeof(WatermarkPasswordBox), new PropertyMetadata(OnWatermarkPropertyChanged));

        private static void OnWatermarkPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            WatermarkPasswordBox watermarkTextBox = sender as WatermarkPasswordBox;
            if (watermarkTextBox != null && watermarkTextBox.WatermarkContent != null)
            {
                watermarkTextBox.DetermineWatermarkContentVisibility();
            }
        }

        public Style WatermarkStyle
        {
            get { return base.GetValue(WatermarkStyleProperty) as Style; }
            set { base.SetValue(WatermarkStyleProperty, value); }
        }

        public static readonly DependencyProperty WatermarkStyleProperty =
            DependencyProperty.Register("WatermarkStyle", typeof(Style), typeof(WatermarkPasswordBox), null);

        public WatermarkPasswordBox()
        {
            DefaultStyleKey = typeof(WatermarkPasswordBox);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.WatermarkContent = this.GetTemplateChild("watermarkContent") as ContentControl;
            this.PasswordContent = this.GetTemplateChild("ContentElement") as PasswordBox;
            if (WatermarkContent != null && WatermarkContent != null)
            {
                PasswordContent.PasswordChanged += new RoutedEventHandler(PasswordContent_PasswordChanged);
                DetermineWatermarkContentVisibility();
            }
        }

        void PasswordContent_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwdBx = sender as PasswordBox;
            this.Text = passwdBx.Password;
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            if (WatermarkContent != null && WatermarkContent != null && string.IsNullOrEmpty(this.PasswordContent.Password))
            {
                this.WatermarkContent.Visibility = Visibility.Collapsed;
            }
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            if (WatermarkContent != null && WatermarkContent != null && string.IsNullOrEmpty(this.PasswordContent.Password))
            {
                this.WatermarkContent.Visibility = Visibility.Visible;
            }
            base.OnLostFocus(e);
        }

        private void DetermineWatermarkContentVisibility()
        {
            if (string.IsNullOrEmpty(this.PasswordContent.Password))
            {
                this.WatermarkContent.Visibility = Visibility.Visible;
            }
            else
            {
                this.WatermarkContent.Visibility = Visibility.Collapsed;
            }
        }
    }
}

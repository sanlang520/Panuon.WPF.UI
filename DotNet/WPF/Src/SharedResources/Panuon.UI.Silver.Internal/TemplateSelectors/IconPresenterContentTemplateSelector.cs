﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Panuon.UI.Silver.Internal.TemplateSelectors
{
    class IconPresenterContentTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null)
            {
                if (item is ImageSource)
                {
                    return CreateImageDataTemplate(item);
                }
                else if (item is string iconString)
                {
                    if (Uri.IsWellFormedUriString(iconString, UriKind.RelativeOrAbsolute))
                    {
                        return CreateImageDataTemplate(item);
                    }
                }
            }

            return CreateContentDataTemplate(item);
        }

        #region Function
        private DataTemplate CreateImageDataTemplate(object item)
        {
            var factory = new FrameworkElementFactory(typeof(Image));
            var imageSource = new Binding() { Source = item };
            factory.SetBinding(Image.SourceProperty, imageSource);
            factory.SetValue(Image.FocusableProperty, false);
            factory.SetValue(RenderOptions.BitmapScalingModeProperty, BitmapScalingMode.HighQuality);
            var dataTemplate = new DataTemplate
            {
                VisualTree = factory
            };
            dataTemplate.Seal();
            return dataTemplate;
        }

        private DataTemplate CreateContentDataTemplate(object item)
        {
            var factory = new FrameworkElementFactory(typeof(ContentControl));
            factory.SetBinding(ContentControl.ContentProperty, new Binding() { Source = item });
            factory.SetValue(ContentControl.FocusableProperty, false);
            var dataTemplate = new DataTemplate
            {
                VisualTree = factory
            };
            dataTemplate.Seal();
            return dataTemplate;
        }
        #endregion

    }

}
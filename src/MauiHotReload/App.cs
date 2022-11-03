﻿using System.Reflection;

namespace MauiHotReload
{
	public partial class App : Application
	{
		public App()
		{
			Initialize();

			MainPage = new NavigationPage(new HomePage());
		}

		private void Initialize()
		{
			var assembly = GetType().Assembly;
			Resources.MergedDictionaries.Add(GetXamlResourceDictionary("Resources/Colors.xaml", assembly));
			Resources.MergedDictionaries.Add(GetXamlResourceDictionary("Resources/Styles.xaml", assembly));
			Resources.Add("ApplePadding", new Thickness(30, 60, 30, 30));
			Resources.Add("DefaultPadding", new Thickness(30));
			Resources.Add("ItemSpacing", 10d);
			Resources.Add(new Style(typeof(StackBase))
			{
				ApplyToDerivedTypes = true,
				Setters =
				{
					new() { Property = StackBase.SpacingProperty, Value = AppResource<double>("ItemSpacing") },
				},
			});
			Resources.Add(new Style(typeof(ContentPage))
			{
				ApplyToDerivedTypes = true,
				Setters =
				{
					new() { Property = Page.PaddingProperty, Value = DeviceInfo.Idiom.ToString() switch { nameof(DeviceIdiom.Phone) => AppResource<Thickness>("ApplePadding"), _ => AppResource<Thickness>("DefaultPadding") } },
				},
			});
			Resources.Add("MauiLabel", new Style(typeof(Label))
			{
				Setters =
				{
					new() { Property = Label.TextColorProperty, Value = RequestedTheme switch { AppTheme.Dark => AppResource<Color>("White"), AppTheme.Light or _ => AppResource<Color>("Primary") } },
				},
			});
			Resources.Add("Action", new Style(typeof(Button))
			{
				Setters =
				{
					new() { Property = Button.BackgroundColorProperty, Value = RequestedTheme switch { AppTheme.Dark => AppResource<Color>("BackgroundDark"), AppTheme.Light or _ => AppResource<Color>("BackgroundLight") } },
					new() { Property = Button.TextColorProperty, Value = RequestedTheme switch { AppTheme.Dark => AppResource<Color>("TextDark"), AppTheme.Light or _ => AppResource<Color>("TextLight") } },
					new() { Property = Button.FontFamilyProperty, Value = AppResource<string>("AppFont") },
					new() { Property = Button.FontSizeProperty, Value = AppResource<double>("AppFontSize") },
					new() { Property = Button.PaddingProperty, Value = new Thickness(14,10) },
				},
			});
			Resources.Add("PrimaryAction", new Style(typeof(Button))
			{
				BasedOn = AppResource<Style>("Action"),
				Setters =
				{
					new() { Property = Button.BackgroundColorProperty, Value = AppResource<Color>("Primary") },
					new() { Property = Button.FontAttributesProperty, Value = FontAttributes.Bold },
					new() { Property = Button.TextColorProperty, Value = AppResource<Color>("White") },
				},
			});
		}

		private static ResourceDictionary GetXamlResourceDictionary(string resourcePath, Assembly assembly)
			=> GetXamlResource<ResourceDictionary>(resourcePath, assembly);
	}
}

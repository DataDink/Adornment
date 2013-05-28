Adornment (0.0.0000000001)
=====================================

by [Mark Nelson](http://www.markonthenet.com/)

This is a .NET WPF helper library that provides XAML wire up for Adorners and the ability to add ui elements to the adorner layer

What it is:
---------------
* XAML friendly
* Get/Set attached DependencyProperty methods for configuring adornments
* Supports single or multiple adornments

What it does:
-------------
Provides a series of XAML friendly attached-property get/set methods for configuring adornments in your UI.
This library allows you to directly add adorners and custom user controls to the adornment layer right in your xaml layout.
Offers improved canvas-like positioning for FrameworkElements added as adornments.

Out of the box:
---------------
* XAML Wireup

*Adding overlaid content to a textblock in your UI:*
The following example domonstrates how to add a "floating" / overlaid translucent border over some text in a window
```html
<Window x:Class="WpfApplication3.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" xmlns:A="clr-namespace:Adorners;assembly=Adorners"
        Title="MainWindow" Height="350" Width="525">
		
    <DockPanel>
        <TextBlock DockPanel.Dock="Top" Text="Content Header" TextAlignment="Center" FontSize="24" />

        <TextBlock Margin="10 25" TextWrapping="Wrap">
			Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce mattis dictum rhoncus. Morbi eu ligula diam, eu dictum turpis. Nullam vel mi eget ante scelerisque bibendum nec vel lacus. Curabitur sollicitudin mauris vitae dui malesuada pulvinar. Cras condimentum, purus vel venenatis rutrum, nisi tortor sollicitudin massa, vitae varius massa quam eu urna. Duis laoreet, urna in vestibulum eleifend, nisl nulla vestibulum metus, eu volutpat enim orci vel mauris. Curabitur blandit, lectus eget blandit euismod, tortor nulla ultrices enim, a tristique elit arcu ut justo.
			Mauris convallis sem nisl, quis gravida lorem. Sed sagittis augue ut mauris scelerisque interdum. Donec commodo libero id enim mattis eu sagittis lorem euismod. Duis dolor odio, feugiat sed interdum sed, vestibulum et nunc. Sed vitae lorem justo. Aliquam dapibus aliquam risus ac dapibus. Integer ac lectus enim. Pellentesque sed dolor dui. In massa mi, rhoncus nec pharetra ultricies, molestie at purus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Sed malesuada risus nec magna eleifend sit amet iaculis lectus scelerisque. Sed blandit enim id nunc aliquam suscipit. Nulla vitae elit urna. Aliquam convallis molestie ipsum sed ultricies.

<!-- Adornment Added Here -->
			<A:Adornment.Content>
                <Border BorderThickness="2"
                        CornerRadius="4"
                        BorderBrush="Black"
                        Background="#80A0A0FF"
                        Width="100"
                        Height="50"
                        A:Adornment.Left="100"
                        A:Adornment.Top="95"/>
            </A:Adornment.Content>
		</TextBlock>
	</DockPanel>
</Window>
```

*Adding multiple adornments to a single element:*
The following example will add two border elements over some text in a TextBlock
```html
<TextBlock TextWrapping="Wrap">
	Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce mattis dictum rhoncus. Morbi eu ligula diam, eu dictum turpis. Nullam vel mi eget ante scelerisque bibendum nec vel lacus. Curabitur sollicitudin mauris vitae dui malesuada pulvinar. Cras condimentum, purus vel venenatis rutrum, nisi tortor sollicitudin massa, vitae varius massa quam eu urna. Duis laoreet, urna in vestibulum eleifend, nisl nulla vestibulum metus, eu volutpat enim orci vel mauris. Curabitur blandit, lectus eget blandit euismod, tortor nulla ultrices enim, a tristique elit arcu ut justo.
	Mauris convallis sem nisl, quis gravida lorem. Sed sagittis augue ut mauris scelerisque interdum. Donec commodo libero id enim mattis eu sagittis lorem euismod. Duis dolor odio, feugiat sed interdum sed, vestibulum et nunc. Sed vitae lorem justo. Aliquam dapibus aliquam risus ac dapibus. Integer ac lectus enim. Pellentesque sed dolor dui. In massa mi, rhoncus nec pharetra ultricies, molestie at purus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Sed malesuada risus nec magna eleifend sit amet iaculis lectus scelerisque. Sed blandit enim id nunc aliquam suscipit. Nulla vitae elit urna. Aliquam convallis molestie ipsum sed ultricies.

<!-- Adornments Added Here -->
	<A:Adornment.Content>
<!-- AdornmentCollection accepts any number of AdornerTemplate or FrameworkElements -->
		<A:AdornmentCollection>
			<Border Width="50" Height="50"
					BorderThickness="2" BorderBrush="Black"
					A:Adornment.Left="50" />
					
			<Border Width="50" Height="50"
					BorderThickness="2" BorderBrush="Black"
					A:Adornment.Left="100" />
		</A:AdornmentCollection>
	</A:Adornment.Content>
</TextBlock>
```

*Adding a custom adorner to an element:*
The following example will a custom Adorner to an element in XAML

The adorner:
```c#
public class GridAdorner : Adorner
{
	public GridAdorner(UIElement adornedElement) : base(adornedElement) {}

	protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
	{
		var owner = AdornedElement as FrameworkElement;
		if (owner == null) return;

		var pen = new Pen(Brushes.Gray, 1);
		for (var x = 0; x < owner.ActualWidth; x += 100) {
			drawingContext.DrawLine(pen, new Point(x, 0), new Point(x, owner.ActualHeight));
		}
		for (var y = 0; y < owner.ActualHeight; y += 100) {
			drawingContext.DrawLine(pen, new Point(0, y), new Point(owner.ActualWidth, y));
		}
	}
}
```

The wireup:
```html
<TextBlock TextWrapping="Wrap">
	Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce mattis dictum rhoncus. Morbi eu ligula diam, eu dictum turpis. Nullam vel mi eget ante scelerisque bibendum nec vel lacus. Curabitur sollicitudin mauris vitae dui malesuada pulvinar. Cras condimentum, purus vel venenatis rutrum, nisi tortor sollicitudin massa, vitae varius massa quam eu urna. Duis laoreet, urna in vestibulum eleifend, nisl nulla vestibulum metus, eu volutpat enim orci vel mauris. Curabitur blandit, lectus eget blandit euismod, tortor nulla ultrices enim, a tristique elit arcu ut justo.
	Mauris convallis sem nisl, quis gravida lorem. Sed sagittis augue ut mauris scelerisque interdum. Donec commodo libero id enim mattis eu sagittis lorem euismod. Duis dolor odio, feugiat sed interdum sed, vestibulum et nunc. Sed vitae lorem justo. Aliquam dapibus aliquam risus ac dapibus. Integer ac lectus enim. Pellentesque sed dolor dui. In massa mi, rhoncus nec pharetra ultricies, molestie at purus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Sed malesuada risus nec magna eleifend sit amet iaculis lectus scelerisque. Sed blandit enim id nunc aliquam suscipit. Nulla vitae elit urna. Aliquam convallis molestie ipsum sed ultricies.
	
<!-- Adornment Added Here -->
	<A:Adornment.Content>
		<A:AdornerTemplate Type="local:GridAdorner"/>
	</A:Adornment.Content>
</TextBlock>
```
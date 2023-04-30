using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VM_Manager
{
    public class Host
    {
        public void add_gpu(StackPanel host_gpu_panel, string host_ip, string gpu_id, Color color_bar_border)
        {
            Grid gpu_grid = new Grid();
            gpu_grid.Height = 110;
            gpu_grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20, GridUnitType.Pixel) });
            gpu_grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(36, GridUnitType.Pixel) });
            gpu_grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            TextBlock gpu_name_textblock = new TextBlock();
            gpu_name_textblock.Name = "host_" + host_ip + "_gpu_name_" + gpu_id;
            gpu_name_textblock.Text = "GPU";
            gpu_name_textblock.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            gpu_name_textblock.FontWeight = FontWeights.Bold;

            Viewbox gpu_name_viewbox = new Viewbox();
            gpu_name_viewbox.VerticalAlignment= VerticalAlignment.Center;
            gpu_name_viewbox.HorizontalAlignment = HorizontalAlignment.Left;
            gpu_name_viewbox.SetValue(Grid.RowProperty, 0);
            gpu_name_viewbox.Child = gpu_name_textblock;

            // [GPU POWER,MEMORY,TEMP,FAN] ------------------------------------------------------------------------------------------------------------------------------------------------------ //
            Grid gpu_stats_grid = new Grid();
            gpu_stats_grid.SetValue(Grid.RowProperty, 1);
            gpu_stats_grid.ColumnDefinitions.Add(new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) });
            gpu_stats_grid.ColumnDefinitions.Add(new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) });
            // [GPU POWER,MEMORY,TEMP,FAN] ------------------------------------------------------------------------------------------------------------------------------------------------------ //

            // [GPU POWER AND MEMORY] ------------------------------------------------------------------------------------------------------------------------------------------------------ //
            Grid gpu_stats_grid_1 = new Grid();
            gpu_stats_grid_1.SetValue(Grid.ColumnProperty, 0);
            gpu_stats_grid_1.ColumnDefinitions.Add(new ColumnDefinition{ Width = new GridLength(60, GridUnitType.Pixel) });
            gpu_stats_grid_1.ColumnDefinitions.Add(new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) });
            gpu_stats_grid_1.ColumnDefinitions.Add(new ColumnDefinition{ Width = new GridLength(60, GridUnitType.Pixel) });
            gpu_stats_grid_1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(16, GridUnitType.Pixel) });
            gpu_stats_grid_1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(16, GridUnitType.Pixel) });

            TextBlock gpu_stats_power = new TextBlock();
            gpu_stats_power.SetValue(Grid.ColumnProperty, 0);
            gpu_stats_power.SetValue(Grid.RowProperty, 0);
            gpu_stats_power.Text = "Power";
            gpu_stats_power.Margin = new Thickness(0, 0, 5, 0);
            gpu_stats_power.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            gpu_stats_power.FontSize = 10;
            gpu_stats_power.FontFamily = new FontFamily("Unispace");
            gpu_stats_power.VerticalAlignment = VerticalAlignment.Center;
            gpu_stats_power.HorizontalAlignment = HorizontalAlignment.Right;
            gpu_stats_grid_1.Children.Add(gpu_stats_power);

            Border gpu_stats_power_bar_border = new Border();
            gpu_stats_power_bar_border.SetValue(Grid.ColumnProperty, 1);
            gpu_stats_power_bar_border.SetValue(Grid.RowProperty, 0);
            gpu_stats_power_bar_border.Name = "host_" + host_ip + "_gpu_power_border_" + gpu_id;
            gpu_stats_power_bar_border.Height = 10;
            gpu_stats_power_bar_border.BorderBrush = new SolidColorBrush(Color.FromRgb(170, 170, 170));
            gpu_stats_power_bar_border.Background = new SolidColorBrush(Color.FromRgb(32, 32, 32));
            gpu_stats_power_bar_border.BorderThickness = new Thickness(2);
            gpu_stats_power_bar_border.CornerRadius = new CornerRadius(3);
            gpu_stats_power_bar_border.VerticalAlignment = VerticalAlignment.Center;

            Rectangle gpu_stats_power_bar = new Rectangle();
            gpu_stats_power_bar.Name = "host_" + host_ip + "_gpu_power_bar_" + gpu_id;
            gpu_stats_power_bar.Width = 0;
            gpu_stats_power_bar.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            gpu_stats_power_bar.HorizontalAlignment = HorizontalAlignment.Left;
            gpu_stats_power_bar_border.Child = gpu_stats_power_bar;
            gpu_stats_grid_1.Children.Add(gpu_stats_power_bar_border);

            TextBlock gpu_stats_power_value = new TextBlock();
            gpu_stats_power_value.SetValue(Grid.ColumnProperty, 2);
            gpu_stats_power_value.SetValue(Grid.RowProperty, 0);
            gpu_stats_power_value.Text = "0W";
            gpu_stats_power_value.Name = "host_" + host_ip + "_gpu_power_" + gpu_id;
            gpu_stats_power_value.Margin = new Thickness(5, 0, 0, 0);
            gpu_stats_power_value.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            gpu_stats_power_value.FontSize = 12;
            gpu_stats_power_value.FontFamily = new FontFamily("Unispace");
            gpu_stats_power_value.VerticalAlignment = VerticalAlignment.Center;
            gpu_stats_power_value.HorizontalAlignment = HorizontalAlignment.Left;
            gpu_stats_grid_1.Children.Add(gpu_stats_power_value);



            TextBlock gpu_stats_memory = new TextBlock();
            gpu_stats_memory.SetValue(Grid.ColumnProperty, 0);
            gpu_stats_memory.SetValue(Grid.RowProperty, 1);
            gpu_stats_memory.Text = "Memory";
            gpu_stats_memory.Margin = new Thickness(0, 0, 5, 0);
            gpu_stats_memory.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            gpu_stats_memory.FontSize = 10;
            gpu_stats_memory.FontFamily = new FontFamily("Unispace");
            gpu_stats_memory.VerticalAlignment = VerticalAlignment.Center;
            gpu_stats_memory.HorizontalAlignment = HorizontalAlignment.Right;
            gpu_stats_grid_1.Children.Add(gpu_stats_memory);

            Border gpu_stats_memory_bar_border = new Border();
            gpu_stats_memory_bar_border.SetValue(Grid.ColumnProperty, 1);
            gpu_stats_memory_bar_border.SetValue(Grid.RowProperty, 1);
            gpu_stats_memory_bar_border.Name = "host_" + host_ip + "_gpu_memory_border_" + gpu_id;
            gpu_stats_memory_bar_border.Height = 10;
            gpu_stats_memory_bar_border.BorderBrush = new SolidColorBrush(Color.FromRgb(170, 170, 170));
            gpu_stats_memory_bar_border.Background = new SolidColorBrush(Color.FromRgb(32, 32, 32));
            gpu_stats_memory_bar_border.BorderThickness = new Thickness(2);
            gpu_stats_memory_bar_border.CornerRadius = new CornerRadius(3);
            gpu_stats_memory_bar_border.VerticalAlignment = VerticalAlignment.Center;

            Rectangle gpu_stats_memory_bar = new Rectangle();
            gpu_stats_memory_bar.Name = "host_" + host_ip + "_gpu_memory_bar_" + gpu_id;
            gpu_stats_memory_bar.Width = 0;
            gpu_stats_memory_bar.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            gpu_stats_memory_bar.HorizontalAlignment = HorizontalAlignment.Left;
            gpu_stats_memory_bar_border.Child = gpu_stats_memory_bar;
            gpu_stats_grid_1.Children.Add(gpu_stats_memory_bar_border);

            TextBlock gpu_stats_memory_value = new TextBlock();
            gpu_stats_memory_value.SetValue(Grid.ColumnProperty, 2);
            gpu_stats_memory_value.SetValue(Grid.RowProperty, 1);
            gpu_stats_memory_value.Text = "0MB";
            gpu_stats_memory_value.Name = "host_" + host_ip + "_gpu_memory_" + gpu_id;
            gpu_stats_memory_value.Margin = new Thickness(5, 0, 0, 0);
            gpu_stats_memory_value.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            gpu_stats_memory_value.FontSize = 12;
            gpu_stats_memory_value.FontFamily = new FontFamily("Unispace");
            gpu_stats_memory_value.VerticalAlignment = VerticalAlignment.Center;
            gpu_stats_memory_value.HorizontalAlignment = HorizontalAlignment.Left;
            gpu_stats_grid_1.Children.Add(gpu_stats_memory_value);
            // [GPU POWER AND MEMORY] ------------------------------------------------------------------------------------------------------------------------------------------------------ //

            // [GPU TEMP AND FAN] ------------------------------------------------------------------------------------------------------------------------------------------------------ //
            Grid gpu_stats_grid_2 = new Grid();
            gpu_stats_grid_2.SetValue(Grid.ColumnProperty, 1);
            gpu_stats_grid_2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(60, GridUnitType.Pixel) });
            gpu_stats_grid_2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            gpu_stats_grid_2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(60, GridUnitType.Pixel) });
            gpu_stats_grid_2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(16, GridUnitType.Pixel) });
            gpu_stats_grid_2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(16, GridUnitType.Pixel) });

            TextBlock gpu_stats_temp = new TextBlock();
            gpu_stats_temp.SetValue(Grid.ColumnProperty, 0);
            gpu_stats_temp.SetValue(Grid.RowProperty, 0);
            gpu_stats_temp.Text = "Temp";
            gpu_stats_temp.Margin = new Thickness(0, 0, 5, 0);
            gpu_stats_temp.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            gpu_stats_temp.FontSize = 10;
            gpu_stats_temp.FontFamily = new FontFamily("Unispace");
            gpu_stats_temp.VerticalAlignment = VerticalAlignment.Center;
            gpu_stats_temp.HorizontalAlignment = HorizontalAlignment.Right;
            gpu_stats_grid_2.Children.Add(gpu_stats_temp);

            Border gpu_stats_temp_bar_border = new Border();
            gpu_stats_temp_bar_border.SetValue(Grid.ColumnProperty, 1);
            gpu_stats_temp_bar_border.SetValue(Grid.RowProperty, 0);
            gpu_stats_temp_bar_border.Name = "host_" + host_ip + "_gpu_temp_border_" + gpu_id;
            gpu_stats_temp_bar_border.Height = 10;
            gpu_stats_temp_bar_border.BorderBrush = new SolidColorBrush(Color.FromRgb(170, 170, 170));
            gpu_stats_temp_bar_border.Background = new SolidColorBrush(Color.FromRgb(32, 32, 32));
            gpu_stats_temp_bar_border.BorderThickness = new Thickness(2);
            gpu_stats_temp_bar_border.CornerRadius = new CornerRadius(3);
            gpu_stats_temp_bar_border.VerticalAlignment = VerticalAlignment.Center;

            Rectangle gpu_stats_temp_bar = new Rectangle();
            gpu_stats_temp_bar.Name = "host_" + host_ip + "_gpu_temp_bar_" + gpu_id;
            gpu_stats_temp_bar.Width = 0;
            gpu_stats_temp_bar.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            gpu_stats_temp_bar.HorizontalAlignment = HorizontalAlignment.Left;
            gpu_stats_temp_bar_border.Child = gpu_stats_temp_bar;
            gpu_stats_grid_2.Children.Add(gpu_stats_temp_bar_border);

            TextBlock gpu_stats_temp_value = new TextBlock();
            gpu_stats_temp_value.SetValue(Grid.ColumnProperty, 2);
            gpu_stats_temp_value.SetValue(Grid.RowProperty, 0);
            gpu_stats_temp_value.Text = "0°C";
            gpu_stats_temp_value.Name = "host_" + host_ip + "_gpu_temp_" + gpu_id;
            gpu_stats_temp_value.Margin = new Thickness(5, 0, 0, 0);
            gpu_stats_temp_value.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            gpu_stats_temp_value.FontSize = 12;
            gpu_stats_temp_value.FontFamily = new FontFamily("Unispace");
            gpu_stats_temp_value.VerticalAlignment = VerticalAlignment.Center;
            gpu_stats_temp_value.HorizontalAlignment = HorizontalAlignment.Left;
            gpu_stats_grid_2.Children.Add(gpu_stats_temp_value);



            TextBlock gpu_stats_fan = new TextBlock();
            gpu_stats_fan.SetValue(Grid.ColumnProperty, 0);
            gpu_stats_fan.SetValue(Grid.RowProperty, 1);
            gpu_stats_fan.Text = "Fan";
            gpu_stats_fan.Margin = new Thickness(0, 0, 5, 0);
            gpu_stats_fan.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            gpu_stats_fan.FontSize = 10;
            gpu_stats_fan.FontFamily = new FontFamily("Unispace");
            gpu_stats_fan.VerticalAlignment = VerticalAlignment.Center;
            gpu_stats_fan.HorizontalAlignment = HorizontalAlignment.Right;
            gpu_stats_grid_2.Children.Add(gpu_stats_fan);

            Border gpu_stats_fan_bar_border = new Border();
            gpu_stats_fan_bar_border.SetValue(Grid.ColumnProperty, 1);
            gpu_stats_fan_bar_border.SetValue(Grid.RowProperty, 1);
            gpu_stats_fan_bar_border.Name = "host_" + host_ip + "_gpu_fan_border_" + gpu_id;
            gpu_stats_fan_bar_border.Height = 10;
            gpu_stats_fan_bar_border.BorderBrush = new SolidColorBrush(Color.FromRgb(170, 170, 170));
            gpu_stats_fan_bar_border.Background = new SolidColorBrush(Color.FromRgb(32, 32, 32));
            gpu_stats_fan_bar_border.BorderThickness = new Thickness(2);
            gpu_stats_fan_bar_border.CornerRadius = new CornerRadius(3);
            gpu_stats_fan_bar_border.VerticalAlignment = VerticalAlignment.Center;

            Rectangle gpu_stats_fan_bar = new Rectangle();
            gpu_stats_fan_bar.Name = "host_" + host_ip + "_gpu_fan_bar_" + gpu_id;
            gpu_stats_fan_bar.Width = 0;
            gpu_stats_fan_bar.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            gpu_stats_fan_bar.HorizontalAlignment = HorizontalAlignment.Left;
            gpu_stats_fan_bar_border.Child = gpu_stats_fan_bar;
            gpu_stats_grid_2.Children.Add(gpu_stats_fan_bar_border);

            TextBlock gpu_stats_fan_value = new TextBlock();
            gpu_stats_fan_value.SetValue(Grid.ColumnProperty, 2);
            gpu_stats_fan_value.SetValue(Grid.RowProperty, 1);
            gpu_stats_fan_value.Text = "0%";
            gpu_stats_fan_value.Name = "host_" + host_ip + "_gpu_fan_" + gpu_id;
            gpu_stats_fan_value.Margin = new Thickness(5, 0, 0, 0);
            gpu_stats_fan_value.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            gpu_stats_fan_value.FontSize = 12;
            gpu_stats_fan_value.FontFamily = new FontFamily("Unispace");
            gpu_stats_fan_value.VerticalAlignment = VerticalAlignment.Center;
            gpu_stats_fan_value.HorizontalAlignment = HorizontalAlignment.Left;
            gpu_stats_grid_2.Children.Add(gpu_stats_fan_value);
            // [GPU TEMP AND FAN] ------------------------------------------------------------------------------------------------------------------------------------------------------ //

            // [GPU MEMORY PER VM] ------------------------------------------------------------------------------------------------------------------------------------------------------ //
            Grid gpu_memory_grid = new Grid();
            gpu_memory_grid.SetValue(Grid.RowProperty, 2);

            Grid gpu_memory_vm_grid = new Grid();
            gpu_memory_vm_grid.Name = "host_" + host_ip + "_gpu_memory_vm_" + gpu_id;

            Border gpu_memory_vm_border = new Border();
            gpu_memory_vm_border.Name = "host_" + host_ip + "_gpu_memory_vm_border_" + gpu_id;
            gpu_memory_vm_border.Height = 24;
            gpu_memory_vm_border.Background = new SolidColorBrush(Color.FromRgb(32, 32, 32));
            gpu_memory_vm_border.BorderBrush = new SolidColorBrush(color_bar_border);
            gpu_memory_vm_border.BorderThickness = new Thickness(2);
            gpu_memory_vm_border.CornerRadius = new CornerRadius(3);
            gpu_memory_vm_border.VerticalAlignment = VerticalAlignment.Top;
            gpu_memory_vm_border.Child = gpu_memory_vm_grid;
            gpu_memory_grid.Children.Add(gpu_memory_vm_border);

            Grid gpu_memory_vm_name = new Grid();
            gpu_memory_vm_name.Name = "host_" + host_ip + "_gpu_memory_vm_name_" + gpu_id;
            gpu_memory_vm_name.Margin = new Thickness(0,20,0,0);
            gpu_memory_vm_name.VerticalAlignment = VerticalAlignment.Top;
            gpu_memory_grid.Children.Add(gpu_memory_vm_name);
            // [GPU MEMORY PER VM] ------------------------------------------------------------------------------------------------------------------------------------------------------ //

            gpu_grid.Children.Add(gpu_name_viewbox);

            gpu_stats_grid.Children.Add(gpu_stats_grid_1);
            gpu_stats_grid.Children.Add(gpu_stats_grid_2);
            gpu_grid.Children.Add(gpu_stats_grid);

            gpu_grid.Children.Add(gpu_memory_grid);

            host_gpu_panel.Children.Add(gpu_grid);
        }

        public Border add_host(string host_ip)
        {
            Grid grid_host = new Grid();
            grid_host.Name = "host_" + host_ip + "_grid";
            grid_host.RowDefinitions.Add(new RowDefinition { Height= new GridLength(20, GridUnitType.Pixel) });
            grid_host.RowDefinitions.Add(new RowDefinition { Height= new GridLength(14, GridUnitType.Pixel) });
            grid_host.RowDefinitions.Add(new RowDefinition { Height= new GridLength(1, GridUnitType.Star) });

            Border border_host = new Border();
            border_host.Margin = new Thickness(5, 5, 5, 5);
            border_host.Padding = new Thickness(5, 0, 5, 0);
            border_host.BorderBrush = new SolidColorBrush(Color.FromRgb(170, 170, 170));
            border_host.BorderThickness = new Thickness(2);
            border_host.CornerRadius = new CornerRadius(3);
            border_host.Child = grid_host;

            // [host name] ------------------------------------------------------------------------------------------------------------------------------------------------------ //
            Border host_name_border = new Border();
            host_name_border.BorderBrush = new SolidColorBrush(Color.FromRgb(170, 170, 170));
            host_name_border.BorderThickness = new Thickness(2);
            host_name_border.CornerRadius= new CornerRadius(3);
            host_name_border.Margin = new Thickness(0, -2, -7, 2);
            host_name_border.Padding = new Thickness(5,0,5,0);
            host_name_border.Height = 20;
            host_name_border.HorizontalAlignment = HorizontalAlignment.Right;

            TextBlock host_name_textblock = new TextBlock();
            host_name_textblock.Name = "host_" + host_ip + "_host_name";
            host_name_textblock.Text = "";
            host_name_textblock.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            host_name_textblock.FontWeight = FontWeights.Bold;

            Viewbox host_name_viewbox = new Viewbox();
            host_name_viewbox.Child = host_name_textblock;

            host_name_border.Child = host_name_viewbox;

            grid_host.Children.Add(host_name_border);
            // [host name] ------------------------------------------------------------------------------------------------------------------------------------------------------ //

            // [cpu name] ------------------------------------------------------------------------------------------------------------------------------------------------------ //
            TextBlock cpu_name_textblock = new TextBlock();
            cpu_name_textblock.Name = "host_" + host_ip + "_cpu_name";
            cpu_name_textblock.Text = "CPU";
            cpu_name_textblock.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            cpu_name_textblock.FontWeight = FontWeights.Bold;

            Viewbox cpu_name_viewbox = new Viewbox();
            cpu_name_viewbox.SetValue(Grid.RowProperty, 0);
            cpu_name_viewbox.VerticalAlignment = VerticalAlignment.Center;
            cpu_name_viewbox.HorizontalAlignment = HorizontalAlignment.Left;
            cpu_name_viewbox.Child = cpu_name_textblock;
            // [cpu name] ------------------------------------------------------------------------------------------------------------------------------------------------------ //
            grid_host.Children.Add(cpu_name_viewbox);

            // [system stats] ------------------------------------------------------------------------------------------------------------------------------------------------------ //
            Grid system_stats_grid = new Grid();
            system_stats_grid.SetValue(Grid.RowProperty, 1);
            system_stats_grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            system_stats_grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            system_stats_grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            Grid system_stats_cpu = new Grid();
            system_stats_cpu.SetValue(Grid.ColumnProperty, 0);
            system_stats_cpu.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50, GridUnitType.Pixel) });
            system_stats_cpu.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            system_stats_cpu.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50, GridUnitType.Pixel) });
            system_stats_cpu.RowDefinitions.Add(new RowDefinition { Height = new GridLength(16, GridUnitType.Pixel) });

            TextBlock system_stats_cpu_name = new TextBlock();
            system_stats_cpu_name.SetValue(Grid.ColumnProperty, 0);
            system_stats_cpu_name.SetValue(Grid.RowProperty, 0);
            system_stats_cpu_name.Text = "CPU";
            system_stats_cpu_name.Margin = new Thickness(0, 0, 5, 0);
            system_stats_cpu_name.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            system_stats_cpu_name.FontSize = 10;
            system_stats_cpu_name.FontFamily = new FontFamily("Unispace");
            system_stats_cpu_name.VerticalAlignment = VerticalAlignment.Center;
            system_stats_cpu_name.HorizontalAlignment = HorizontalAlignment.Right;
            system_stats_cpu.Children.Add(system_stats_cpu_name);

            Rectangle system_stats_cpu_bar = new Rectangle();
            system_stats_cpu_bar.Name = "host_" + host_ip + "_cpu_bar";
            system_stats_cpu_bar.Width = 0;
            system_stats_cpu_bar.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            system_stats_cpu_bar.HorizontalAlignment = HorizontalAlignment.Left;

            Border system_stats_cpu_border = new Border();
            system_stats_cpu_border.Name = "host_" + host_ip + "_cpu_border";
            system_stats_cpu_border.SetValue(Grid.ColumnProperty, 1);
            system_stats_cpu_border.SetValue(Grid.RowProperty, 0);
            system_stats_cpu_border.Height = 10;
            system_stats_cpu_border.BorderBrush = new SolidColorBrush(Color.FromRgb(170, 170, 170));
            system_stats_cpu_border.Background = new SolidColorBrush(Color.FromRgb(32, 32, 32));
            system_stats_cpu_border.BorderThickness = new Thickness(2);
            system_stats_cpu_border.CornerRadius = new CornerRadius(3);
            system_stats_cpu_border.VerticalAlignment = VerticalAlignment.Center;
            system_stats_cpu_border.Child = system_stats_cpu_bar;
            system_stats_cpu.Children.Add(system_stats_cpu_border);

            TextBlock system_stats_cpu_value = new TextBlock();
            system_stats_cpu_value.Name = "host_" + host_ip + "_cpu_value";
            system_stats_cpu_value.SetValue(Grid.ColumnProperty, 2);
            system_stats_cpu_value.SetValue(Grid.RowProperty, 0);
            system_stats_cpu_value.Text = "0%";
            system_stats_cpu_value.Margin = new Thickness(5, 0, 0, 0);
            system_stats_cpu_value.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            system_stats_cpu_value.FontSize = 12;
            system_stats_cpu_value.FontFamily = new FontFamily("Unispace");
            system_stats_cpu_value.VerticalAlignment = VerticalAlignment.Center;
            system_stats_cpu_value.HorizontalAlignment = HorizontalAlignment.Left;
            system_stats_cpu.Children.Add(system_stats_cpu_value);

            system_stats_grid.Children.Add(system_stats_cpu);



            Grid system_stats_ram = new Grid();
            system_stats_ram.SetValue(Grid.ColumnProperty, 1);
            system_stats_ram.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50, GridUnitType.Pixel) });
            system_stats_ram.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            system_stats_ram.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50, GridUnitType.Pixel) });
            system_stats_ram.RowDefinitions.Add(new RowDefinition { Height = new GridLength(16, GridUnitType.Pixel) });

            TextBlock system_stats_ram_name = new TextBlock();
            system_stats_ram_name.SetValue(Grid.ColumnProperty, 0);
            system_stats_ram_name.SetValue(Grid.RowProperty, 0);
            system_stats_ram_name.Text = "Ram";
            system_stats_ram_name.Margin = new Thickness(0, 0, 5, 0);
            system_stats_ram_name.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            system_stats_ram_name.FontSize = 10;
            system_stats_ram_name.FontFamily = new FontFamily("Unispace");
            system_stats_ram_name.VerticalAlignment = VerticalAlignment.Center;
            system_stats_ram_name.HorizontalAlignment = HorizontalAlignment.Right;
            system_stats_ram.Children.Add(system_stats_ram_name);

            Rectangle system_stats_ram_bar = new Rectangle();
            system_stats_ram_bar.Name = "host_" + host_ip + "_ram_bar";
            system_stats_ram_bar.Width = 0;
            system_stats_ram_bar.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            system_stats_ram_bar.HorizontalAlignment = HorizontalAlignment.Left;

            Border system_stats_ram_border = new Border();
            system_stats_ram_border.Name = "host_" + host_ip + "_ram_border";
            system_stats_ram_border.SetValue(Grid.ColumnProperty, 1);
            system_stats_ram_border.SetValue(Grid.RowProperty, 0);
            system_stats_ram_border.Height = 10;
            system_stats_ram_border.BorderBrush = new SolidColorBrush(Color.FromRgb(170, 170, 170));
            system_stats_ram_border.Background = new SolidColorBrush(Color.FromRgb(32, 32, 32));
            system_stats_ram_border.BorderThickness = new Thickness(2);
            system_stats_ram_border.CornerRadius = new CornerRadius(3);
            system_stats_ram_border.VerticalAlignment = VerticalAlignment.Center;
            system_stats_ram_border.Child = system_stats_ram_bar;
            system_stats_ram.Children.Add(system_stats_ram_border);

            TextBlock system_stats_ram_value = new TextBlock();
            system_stats_ram_value.Name = "host_" + host_ip + "_ram_value";
            system_stats_ram_value.SetValue(Grid.ColumnProperty, 2);
            system_stats_ram_value.SetValue(Grid.RowProperty, 0);
            system_stats_ram_value.Text = "0%";
            system_stats_ram_value.Margin = new Thickness(5, 0, 0, 0);
            system_stats_ram_value.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            system_stats_ram_value.FontSize = 12;
            system_stats_ram_value.FontFamily = new FontFamily("Unispace");
            system_stats_ram_value.VerticalAlignment = VerticalAlignment.Center;
            system_stats_ram_value.HorizontalAlignment = HorizontalAlignment.Left;
            system_stats_ram.Children.Add(system_stats_ram_value);

            system_stats_grid.Children.Add(system_stats_ram);



            Grid system_stats_temp = new Grid();
            system_stats_temp.SetValue(Grid.ColumnProperty, 2);
            system_stats_temp.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50, GridUnitType.Pixel) });
            system_stats_temp.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            system_stats_temp.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50, GridUnitType.Pixel) });
            system_stats_temp.RowDefinitions.Add(new RowDefinition { Height = new GridLength(16, GridUnitType.Pixel) });

            TextBlock system_stats_temp_name = new TextBlock();
            system_stats_temp_name.SetValue(Grid.ColumnProperty, 0);
            system_stats_temp_name.SetValue(Grid.RowProperty, 0);
            system_stats_temp_name.Text = "Temp";
            system_stats_temp_name.Margin = new Thickness(0, 0, 5, 0);
            system_stats_temp_name.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            system_stats_temp_name.FontSize = 10;
            system_stats_temp_name.FontFamily = new FontFamily("Unispace");
            system_stats_temp_name.VerticalAlignment = VerticalAlignment.Center;
            system_stats_temp_name.HorizontalAlignment = HorizontalAlignment.Right;
            system_stats_temp.Children.Add(system_stats_temp_name);

            Rectangle system_stats_temp_bar = new Rectangle();
            system_stats_temp_bar.Name = "host_" + host_ip + "_temp_bar";
            system_stats_temp_bar.Width = 0;
            system_stats_temp_bar.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            system_stats_temp_bar.HorizontalAlignment = HorizontalAlignment.Left;

            Border system_stats_temp_border = new Border();
            system_stats_temp_border.Name = "host_" + host_ip + "_temp_border";
            system_stats_temp_border.SetValue(Grid.ColumnProperty, 1);
            system_stats_temp_border.SetValue(Grid.RowProperty, 0);
            system_stats_temp_border.Height = 10;
            system_stats_temp_border.BorderBrush = new SolidColorBrush(Color.FromRgb(170, 170, 170));
            system_stats_temp_border.Background = new SolidColorBrush(Color.FromRgb(32, 32, 32));
            system_stats_temp_border.BorderThickness = new Thickness(2);
            system_stats_temp_border.CornerRadius = new CornerRadius(3);
            system_stats_temp_border.VerticalAlignment = VerticalAlignment.Center;
            system_stats_temp_border.Child = system_stats_temp_bar;
            system_stats_temp.Children.Add(system_stats_temp_border);

            TextBlock system_stats_temp_value = new TextBlock();
            system_stats_temp_value.Name = "host_" + host_ip + "_temp_value";
            system_stats_temp_value.SetValue(Grid.ColumnProperty, 2);
            system_stats_temp_value.SetValue(Grid.RowProperty, 0);
            system_stats_temp_value.Text = "0%";
            system_stats_temp_value.Margin = new Thickness(5, 0, 0, 0);
            system_stats_temp_value.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            system_stats_temp_value.FontSize = 12;
            system_stats_temp_value.FontFamily = new FontFamily("Unispace");
            system_stats_temp_value.VerticalAlignment = VerticalAlignment.Center;
            system_stats_temp_value.HorizontalAlignment = HorizontalAlignment.Left;
            system_stats_temp.Children.Add(system_stats_temp_value);

            system_stats_grid.Children.Add(system_stats_temp);
            // [system stats] ------------------------------------------------------------------------------------------------------------------------------------------------------ //

            StackPanel stackpanel_gpu = new StackPanel();
            stackpanel_gpu.Margin = new Thickness(0, 5, 0, 0);//new
            stackpanel_gpu.SetValue(Grid.RowProperty, 2);
            stackpanel_gpu.Name = "host_" + host_ip + "_gpu";

            grid_host.Children.Add(system_stats_grid);

            grid_host.Children.Add(stackpanel_gpu);

            return border_host;
        }



    }
}

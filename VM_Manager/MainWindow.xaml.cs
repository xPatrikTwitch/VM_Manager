using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace VM_Manager
{
    public partial class MainWindow : Window
    {
        List<host_data> host_info = new List<host_data>();
        List<string> host_ip_list = new List<string>();

        string version_string = "1.0.0";

        int monitoring_refresh_rate_ms = 500;
        int cpu_max_temp = 90;
        int gpu_max_temp = 80;

        Color color_background = (Color)ColorConverter.ConvertFromString("#282828");
        Color color_bar_background = (Color)ColorConverter.ConvertFromString("#282828");
        Color color_bar_border = (Color)ColorConverter.ConvertFromString("#aaaaaa");

        Color color_cpu_usage = (Color)ColorConverter.ConvertFromString("#46c948");
        Color color_ram_usage = (Color)ColorConverter.ConvertFromString("#468cc9");
        Color color_cpu_temperature = (Color)ColorConverter.ConvertFromString("#ff8800");

        Color color_gpu_power_draw = (Color)ColorConverter.ConvertFromString("#ff4a4a");
        Color color_gpu_memory_usage = (Color)ColorConverter.ConvertFromString("#46c948");
        Color color_gpu_temperature = (Color)ColorConverter.ConvertFromString("#ff8800");
        Color color_gpu_fan = (Color)ColorConverter.ConvertFromString("#468cc9");

        Color color_gpu_memory_bar_used = (Color)ColorConverter.ConvertFromString("#46c948");
        Color color_gpu_memory_bar_free = (Color)ColorConverter.ConvertFromString("#468cc9");

        Host host_ui_manager = new Host();

        public MainWindow()
        {
            InitializeComponent();

            if (File.Exists(Environment.CurrentDirectory + "/config.json"))
            {
                read_config();
            }
            else
            {
                generate_config();
            }

            this.Title = "VM_Manager " + version_string;
            this.Background = new SolidColorBrush(color_background);

            if (File.Exists(Environment.CurrentDirectory + "/hosts.txt"))
            {
                using (StreamReader sr = new StreamReader(Environment.CurrentDirectory + "/hosts.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        host_data host_data = get_api_data(line.TrimEnd());
                        if (host_data != null)
                        {
                            host_panel.Children.Add(host_ui_manager.add_host(line.TrimEnd().Replace(".", "_").Replace(":", "_")));
                            host_info.Add(host_data);
                            host_ip_list.Add(line.TrimEnd());
                        }
                        else
                        {
                            MessageBox.Show(line.TrimEnd() + " is offline!");
                        }
                    }
                }
            }
            else
            {
                File.Create(Environment.CurrentDirectory + "/hosts.txt");
                MessageBox.Show("No hosts specified, add each host to new line in hosts.txt");
            }

            System.Timers.Timer timer_monitoring = new System.Timers.Timer();
            timer_monitoring.Interval = monitoring_refresh_rate_ms;
            timer_monitoring.Elapsed += timer_monitoring_Tick;
            timer_monitoring.Start();

            update_host_info(true);
        }

        private void read_config()
        {
            try
            {
                string config_json_string = "";
                using (StreamReader sr = new StreamReader(Environment.CurrentDirectory + "/config.json"))
                {
                    config_json_string = sr.ReadToEnd();
                }
                JObject config_json = JObject.Parse(config_json_string);
                monitoring_refresh_rate_ms = (int)config_json.SelectToken("monitoring_refresh_rate_ms");
                cpu_max_temp = (int)config_json.SelectToken("cpu_max_temp");
                gpu_max_temp = (int)config_json.SelectToken("gpu_max_temp");
                color_background = (Color)ColorConverter.ConvertFromString((string)config_json.SelectToken("color_background"));
                color_bar_background = (Color)ColorConverter.ConvertFromString((string)config_json.SelectToken("color_bar_background"));
                color_bar_border = (Color)ColorConverter.ConvertFromString((string)config_json.SelectToken("color_bar_border"));
                color_cpu_usage = (Color)ColorConverter.ConvertFromString((string)config_json.SelectToken("color_cpu_usage "));
                color_ram_usage = (Color)ColorConverter.ConvertFromString((string)config_json.SelectToken("color_ram_usage"));
                color_cpu_temperature = (Color)ColorConverter.ConvertFromString((string)config_json.SelectToken("color_cpu_temperature"));
                color_gpu_power_draw = (Color)ColorConverter.ConvertFromString((string)config_json.SelectToken("color_gpu_power_draw"));
                color_gpu_memory_usage = (Color)ColorConverter.ConvertFromString((string)config_json.SelectToken("color_gpu_memory_usage"));
                color_gpu_temperature = (Color)ColorConverter.ConvertFromString((string)config_json.SelectToken("color_gpu_temperature"));
                color_gpu_fan = (Color)ColorConverter.ConvertFromString((string)config_json.SelectToken("color_gpu_fan"));
                color_gpu_memory_bar_used = (Color)ColorConverter.ConvertFromString((string)config_json.SelectToken("color_gpu_memory_bar_used"));
                color_gpu_memory_bar_free = (Color)ColorConverter.ConvertFromString((string)config_json.SelectToken("color_gpu_memory_bar_free"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to read config (Delete config file to reset to default settings) " + ex.ToString());
                this.Close();
            }
        }

        private void generate_config()
        {
            json_config default_config = new json_config()
            {
                monitoring_refresh_rate_ms = monitoring_refresh_rate_ms,
                cpu_max_temp = cpu_max_temp,
                gpu_max_temp = gpu_max_temp,
                color_background = color_to_string(color_background),
                color_bar_background = color_to_string(color_bar_background),
                color_bar_border = color_to_string(color_bar_border),
                color_cpu_usage = color_to_string(color_cpu_usage),
                color_ram_usage = color_to_string(color_ram_usage),
                color_cpu_temperature = color_to_string(color_cpu_temperature),
                color_gpu_power_draw = color_to_string(color_gpu_power_draw),
                color_gpu_memory_usage = color_to_string(color_gpu_memory_usage),
                color_gpu_temperature = color_to_string(color_gpu_temperature),
                color_gpu_fan = color_to_string(color_gpu_fan),
                color_gpu_memory_bar_used = color_to_string(color_gpu_memory_bar_used),
                color_gpu_memory_bar_free = color_to_string(color_gpu_memory_bar_free),

            };
            string config_json_string = JsonConvert.SerializeObject(default_config, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "/config.json"))
            {
                sw.Write(config_json_string);
            }
        }

        public void timer_monitoring_Tick(Object source, System.Timers.ElapsedEventArgs e)
        {
            host_info.Clear();
            foreach (string host in host_ip_list)
            {
                host_data host_data = get_api_data(host);
                host_info.Add(host_data);
            }
            update_host_info(false);
        }

        private void update_host_info(bool host_create)
        {
            int host_id = 0;
            foreach (host_data host in host_info)
            {
                if (host == null) { continue; }
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    StackPanel gpu_panel = (StackPanel)((Grid)((Border)host_panel.Children[host_id]).Child).Children[3];
                    TextBlock host_name = (TextBlock)((Viewbox)((Border)((Grid)((Border)host_panel.Children[host_id]).Child).Children[0]).Child).Child;
                    TextBlock cpu_name = (TextBlock)((Viewbox)((Grid)((Border)host_panel.Children[host_id]).Child).Children[1]).Child;

                    TextBlock cpu_value = (TextBlock)((Grid)((Grid)((Grid)((Border)host_panel.Children[host_id]).Child).Children[2]).Children[0]).Children[2];
                    Border cpu_border = (Border)((Grid)((Grid)((Grid)((Border)host_panel.Children[host_id]).Child).Children[2]).Children[0]).Children[1];

                    TextBlock ram_value = (TextBlock)((Grid)((Grid)((Grid)((Border)host_panel.Children[host_id]).Child).Children[2]).Children[1]).Children[2];
                    Border ram_border = (Border)((Grid)((Grid)((Grid)((Border)host_panel.Children[host_id]).Child).Children[2]).Children[1]).Children[1];

                    TextBlock temp_value = (TextBlock)((Grid)((Grid)((Grid)((Border)host_panel.Children[host_id]).Child).Children[2]).Children[2]).Children[2];
                    Border temp_border = (Border)((Grid)((Grid)((Grid)((Border)host_panel.Children[host_id]).Child).Children[2]).Children[2]).Children[1];

                    if (host_create == true)
                    {
                        int gpu_id = 0;
                        foreach (gpu gpu in host.gpu)
                        {
                            host_ui_manager.add_gpu(gpu_panel, host.host_ip.Replace(".", "_").Replace(":", "_"), gpu_id.ToString(), color_bar_border);
                            gpu_id++;
                        }
                    }

                    if (host_create == false)
                    {
                        cpu_name.Text = host.cpu_name + " — " + host.cpu_frequency + "MHz";
                        host_name.Text = host.host_name + " — " + host.host_ip + " [" + host.app_version + "]";

                        cpu_value.Text = host.cpu_usage + "%";
                        ram_value.Text = host.ram_usage + "GB";
                        temp_value.Text = host.cpu_temperature + "°C";

                        set_bar_value(Convert.ToDouble(host.cpu_usage), 100, (Rectangle)cpu_border.Child, cpu_border, color_cpu_usage, color_bar_border);
                        set_bar_value(Convert.ToDouble(host.ram_usage), Convert.ToDouble(host.ram_total), (Rectangle)ram_border.Child, ram_border, color_ram_usage, color_bar_border);
                        set_bar_value(Convert.ToDouble(host.cpu_temperature), cpu_max_temp, (Rectangle)temp_border.Child, temp_border, color_cpu_temperature, color_bar_border);

                        int gpu_id = 0;
                        foreach (gpu gpu in host.gpu)
                        {
                            string gpu_type_string = "";

                            if (host.gpu[gpu_id].gpu_mdev.ToLower() == "true")
                            {
                                if (host.gpu[gpu_id].gpu_vendor.ToLower() == "10de")
                                {
                                    gpu_type_string = "Nvidia vGPU (Shared)";
                                }
                                if (host.gpu[gpu_id].gpu_vendor.ToLower() == "1002")
                                {
                                    gpu_type_string = "Amd (Shared)";
                                }
                                if (host.gpu[gpu_id].gpu_vendor.ToLower() == "8086")
                                {
                                    gpu_type_string = "Intel (Shared)";
                                }
                            }
                            else
                            {
                                gpu_type_string = "Passthrough (Dedicated)";
                            }
       
                            string gpu_vendor_string = "";
                            if (host.gpu[gpu_id].gpu_vendor == "0") { gpu_vendor_string = "Nvidia"; }
                            if (host.gpu[gpu_id].gpu_vendor == "1") { gpu_vendor_string = "Amd"; }
                            if (host.gpu[gpu_id].gpu_vendor == "2") { gpu_vendor_string = "Intel"; }

                            TextBlock gpu_name = (TextBlock)((Viewbox)((Grid)gpu_panel.Children[gpu_id]).Children[0]).Child;

                            gpu_name.Text = gpu_vendor_string + " " + host.gpu[gpu_id].gpu_name + " — " + gpu_type_string;

                            TextBlock gpu_power_value = (TextBlock)((Grid)((Grid)((Grid)gpu_panel.Children[gpu_id]).Children[1]).Children[0]).Children[2];
                            TextBlock gpu_memory_value = (TextBlock)((Grid)((Grid)((Grid)gpu_panel.Children[gpu_id]).Children[1]).Children[0]).Children[5];
                            TextBlock gpu_temp_value = (TextBlock)((Grid)((Grid)((Grid)gpu_panel.Children[gpu_id]).Children[1]).Children[1]).Children[2];
                            TextBlock gpu_fan_value = (TextBlock)((Grid)((Grid)((Grid)gpu_panel.Children[gpu_id]).Children[1]).Children[1]).Children[5];

                            gpu_power_value.Text = host.gpu[gpu_id].gpu_power_draw + "W";
                            gpu_memory_value.Text = host.gpu[gpu_id].gpu_memory_usage + "MB";
                            gpu_temp_value.Text = host.gpu[gpu_id].gpu_temperature + "°C";
                            gpu_fan_value.Text = host.gpu[gpu_id].gpu_fan + "%";

                            Border gpu_power_border = (Border)((Grid)((Grid)((Grid)gpu_panel.Children[gpu_id]).Children[1]).Children[0]).Children[1];
                            Border gpu_memory_border = (Border)((Grid)((Grid)((Grid)gpu_panel.Children[gpu_id]).Children[1]).Children[0]).Children[4];
                            Border gpu_temp_border = (Border)((Grid)((Grid)((Grid)gpu_panel.Children[gpu_id]).Children[1]).Children[1]).Children[1];
                            Border gpu_fan_border = (Border)((Grid)((Grid)((Grid)gpu_panel.Children[gpu_id]).Children[1]).Children[1]).Children[4];

                            set_bar_value(Convert.ToDouble(host.gpu[gpu_id].gpu_power_draw), Convert.ToDouble(host.gpu[gpu_id].gpu_power_limit), (Rectangle)gpu_power_border.Child, gpu_power_border, color_gpu_power_draw, color_bar_border);
                            set_bar_value(Convert.ToDouble(host.gpu[gpu_id].gpu_memory_usage), Convert.ToDouble(host.gpu[gpu_id].gpu_memory_total), (Rectangle)gpu_memory_border.Child, gpu_memory_border, color_gpu_memory_usage, color_bar_border);
                            set_bar_value(Convert.ToDouble(host.gpu[gpu_id].gpu_temperature), gpu_max_temp, (Rectangle)gpu_temp_border.Child, gpu_temp_border, color_gpu_temperature, color_bar_border);
                            set_bar_value(Convert.ToDouble(host.gpu[gpu_id].gpu_fan), 100, (Rectangle)gpu_fan_border.Child, gpu_fan_border, color_gpu_fan, color_bar_border);

                            Grid gpu_vm_memory = (Grid)((Border)((Grid)(Grid)((Grid)gpu_panel.Children[gpu_id]).Children[2]).Children[0]).Child;
                            Grid gpu_vm_memory_name = (Grid)((Grid)((Grid)gpu_panel.Children[gpu_id]).Children[2]).Children[1];

                            gpu_vm_memory.Children.Clear();
                            gpu_vm_memory_name.Children.Clear();

                            int last_width = 0;
                            int vm_gpu_memory_total = 0;
                            double width_per_mb = gpu_vm_memory.ActualWidth / int.Parse(host.gpu[gpu_id].gpu_memory_total);
                            foreach (vm vm in host.gpu[gpu_id].gpu_vm)
                            {
                                if (vm.vm_state != "1") { continue; }

                                int vm_gpu_memory = int.Parse(vm.vm_gpu_memory);
                                vm_gpu_memory_total = vm_gpu_memory_total + vm_gpu_memory;

                                Viewbox vm_memory_usage_block_name_vb = new Viewbox();

                                TextBlock vm_memory_usage_block_name = new TextBlock();
                                if (vm_gpu_memory == 1)
                                {
                                    vm_memory_usage_block_name.Text = "?" + "G";
                                }
                                else
                                {
                                    vm_memory_usage_block_name.Text = (vm_gpu_memory / 1024).ToString() + "G";
                                }
                                vm_memory_usage_block_name.VerticalAlignment = VerticalAlignment.Center;
                                vm_memory_usage_block_name.HorizontalAlignment = HorizontalAlignment.Center;
                                vm_memory_usage_block_name.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                                vm_memory_usage_block_name_vb.Child = vm_memory_usage_block_name;

                                Border vm_memory_usage_block = new Border();
                                vm_memory_usage_block.Margin = new Thickness(last_width, 0, 0, 0);
                                vm_memory_usage_block.Width = Math.Floor(vm_gpu_memory * width_per_mb);
                                vm_memory_usage_block.HorizontalAlignment = HorizontalAlignment.Left;
                                vm_memory_usage_block.Background = new SolidColorBrush(color_gpu_memory_bar_used);

                                vm_memory_usage_block.BorderBrush = new SolidColorBrush(color_bar_border);
                                vm_memory_usage_block.BorderThickness = new Thickness(1);
                                vm_memory_usage_block.Child = vm_memory_usage_block_name_vb;

                                TextBlock vm_memory_usege_block_name_2 = new TextBlock();
                                vm_memory_usege_block_name_2.Text = "— " + vm.vm_id;
                                vm_memory_usege_block_name_2.Margin = new Thickness(7 + last_width + Math.Floor(vm_gpu_memory * width_per_mb) / 2, 0, 0, 0);
                                vm_memory_usege_block_name_2.FontSize = 11;
                                vm_memory_usege_block_name_2.VerticalAlignment = VerticalAlignment.Center;
                                vm_memory_usege_block_name_2.HorizontalAlignment = HorizontalAlignment.Left;
                                vm_memory_usege_block_name_2.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                                vm_memory_usege_block_name_2.RenderTransform = new RotateTransform(35);

                                gpu_vm_memory_name.Children.Add(vm_memory_usege_block_name_2);

                                last_width = last_width + Convert.ToInt32(Math.Floor(vm_gpu_memory * width_per_mb));

                                gpu_vm_memory.Children.Add(vm_memory_usage_block);
                            }

                            int free_memory = int.Parse(host.gpu[gpu_id].gpu_memory_total) - int.Parse(host.gpu[gpu_id].gpu_memory_usage);

                            Viewbox vm_memory_usage_block_name_vb_free = new Viewbox();

                            TextBlock vm_memory_usage_block_name_free = new TextBlock();
                            if (free_memory == 1)
                            {
                                vm_memory_usage_block_name_free.Text = "?" + "MB";
                            }
                            else
                            {
                                vm_memory_usage_block_name_free.Text = free_memory.ToString() + "MB";
                            }
                            vm_memory_usage_block_name_free.VerticalAlignment = VerticalAlignment.Center;
                            vm_memory_usage_block_name_free.HorizontalAlignment = HorizontalAlignment.Center;
                            vm_memory_usage_block_name_free.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                            vm_memory_usage_block_name_vb_free.Child = vm_memory_usage_block_name_free;

                            Border vm_memory_usage_block_free = new Border();
                            vm_memory_usage_block_free.Margin = new Thickness(last_width, 0, 0, 0);
                            vm_memory_usage_block_free.Width = gpu_vm_memory.ActualWidth - last_width;
                            vm_memory_usage_block_free.HorizontalAlignment = HorizontalAlignment.Left;
                            vm_memory_usage_block_free.Background = new SolidColorBrush(color_gpu_memory_bar_free);
                            vm_memory_usage_block_free.BorderBrush = new SolidColorBrush(color_bar_border);
                            vm_memory_usage_block_free.BorderThickness = new Thickness(1);
                            vm_memory_usage_block_free.Child = vm_memory_usage_block_name_vb_free;

                            TextBlock vm_memory_usege_block_name_2_free = new TextBlock();
                            vm_memory_usege_block_name_2_free.Text = "— " + "Free";
                            vm_memory_usege_block_name_2_free.Margin = new Thickness(7 + last_width + Math.Floor(free_memory * width_per_mb) / 2, 0, 0, 0);
                            vm_memory_usege_block_name_2_free.FontSize = 11;
                            vm_memory_usege_block_name_2_free.VerticalAlignment = VerticalAlignment.Center;
                            vm_memory_usege_block_name_2_free.HorizontalAlignment = HorizontalAlignment.Left;
                            vm_memory_usege_block_name_2_free.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                            vm_memory_usege_block_name_2_free.RenderTransform = new RotateTransform(35);

                            gpu_vm_memory_name.Children.Add(vm_memory_usege_block_name_2_free);
                            gpu_vm_memory.Children.Add(vm_memory_usage_block_free);
                            gpu_id = gpu_id + 1;
                        }
                    }
                    host_id = host_id + 1;
                }));
            }
        }

        private void set_bar_value(double value, double max, Rectangle bar, Border border, Color bar_color, Color border_color)
        {
            bar.Fill = new SolidColorBrush(bar_color);
            border.Background = new SolidColorBrush(color_bar_background);
            border.BorderBrush = new SolidColorBrush(border_color);
            double width = border.ActualWidth - (border.BorderThickness.Left + border.BorderThickness.Right);
            double value_multiplier = width / max;
            double bar_width = value * value_multiplier;
            bar_width = Math.Floor(bar_width);
            if (bar_width >= 0 && bar_width <= width) { bar.Width = bar_width; }
            if (bar_width > width) { bar.Width = width; }
        }

        private host_data get_api_data(string ip)
        {
            try
            {
                string response;
                StreamReader inStream;
                WebRequest webRequest;
                WebResponse webResponse;
                webRequest = WebRequest.Create("http://" + ip);
                webRequest.Timeout = 250;
                webResponse = webRequest.GetResponse();
                inStream = new StreamReader(webResponse.GetResponseStream());
                response = inStream.ReadToEnd();

                JObject response_json = JObject.Parse(response);

                List<gpu> gpu_list = new List<gpu>();

                foreach (JObject gpu in response_json.SelectToken("gpu"))
                {
                    List<vm> vm_list = new List<vm>();
                    foreach (JObject vm in gpu.SelectToken("gpu_vm"))
                    {
                        vm vm_object = new vm();
                        vm_object.vm_id = (string)vm.SelectToken("vm_id");
                        vm_object.vm_state = (string)vm.SelectToken("vm_state");
                        vm_object.vm_gpu_memory = (string)vm.SelectToken("vm_gpu_memory");
                        if (vm_object.vm_gpu_memory == "0")
                        {
                            vm_object.vm_gpu_memory = "1";
                        }
                        vm_list.Add(vm_object);
                    }
                    gpu gpu_object = new gpu();
                    gpu_object.gpu_name = (string)gpu.SelectToken("gpu_name");
                    gpu_object.gpu_pci_id = (string)gpu.SelectToken("gpu_pci_id");
                    gpu_object.gpu_mdev = (string)gpu.SelectToken("gpu_mdev");
                    gpu_object.gpu_vendor = (string)gpu.SelectToken("gpu_vendor");
                    gpu_object.gpu_device = (string)gpu.SelectToken("gpu_device");
                    gpu_object.gpu_power_draw = (string)gpu.SelectToken("gpu_power_draw");
                    gpu_object.gpu_power_limit = (string)gpu.SelectToken("gpu_power_limit");
                    gpu_object.gpu_memory_usage = (string)gpu.SelectToken("gpu_memory_usage");
                    gpu_object.gpu_memory_total = (string)gpu.SelectToken("gpu_memory_total");
                    gpu_object.gpu_temperature = (string)gpu.SelectToken("gpu_temperature");
                    gpu_object.gpu_fan = (string)gpu.SelectToken("gpu_fan");
                    
                    if (gpu_object.gpu_memory_total == "0")
                    {
                        gpu_object.gpu_memory_total = "1"; //Fix when gpu memory is unknown (can heppen in passthrough mode)
                    }
                    gpu_object.gpu_vm = vm_list;
                    gpu_list.Add(gpu_object);
                }

                host_data host_data_list = new host_data();
                host_data_list.host_ip = ip;
                host_data_list.app_version = (string)response_json.SelectToken("app_version");
                host_data_list.host_name = (string)response_json.SelectToken("host_name");
                host_data_list.cpu_name = (string)response_json.SelectToken("cpu_name");
                host_data_list.cpu_usage = (string)response_json.SelectToken("cpu_usage");
                host_data_list.cpu_frequency = (string)response_json.SelectToken("cpu_frequency");
                host_data_list.cpu_temperature = (string)response_json.SelectToken("cpu_temperature");
                host_data_list.ram_usage = (string)response_json.SelectToken("ram_usage");
                host_data_list.ram_total = (string)response_json.SelectToken("ram_total");
                host_data_list.gpu = gpu_list;

                return host_data_list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        private string color_to_string(Color color)
        {
            return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }

        public class json_config
        {
            public int monitoring_refresh_rate_ms { get; set; }
            public int cpu_max_temp { get; set; }
            public int gpu_max_temp { get; set; }
            public string color_background { get; set; }
            public string color_bar_background { get; set; }
            public string color_bar_border { get; set; }
            public string color_cpu_usage { get; set; }
            public string color_ram_usage { get; set; }
            public string color_cpu_temperature { get; set; }
            public string color_gpu_power_draw { get; set; }
            public string color_gpu_memory_usage { get; set; }
            public string color_gpu_temperature { get; set; }
            public string color_gpu_fan { get; set; }
            public string color_gpu_memory_bar_used { get; set; }
            public string color_gpu_memory_bar_free { get; set; }
        }

        public class host_data
        {
            public string app_version { get; set; }
            public string host_name { get; set; }
            public string host_ip { get; set; }
            public string cpu_name { get; set; }
            public string cpu_usage { get; set; }
            public string cpu_frequency { get; set; }
            public string cpu_temperature { get; set; }
            public string ram_usage { get; set; }
            public string ram_total { get; set; }
            public List<gpu> gpu { get; set; }
        }

        public class gpu
        {
            public string gpu_name { get; set; }
            public string gpu_pci_id { get; set; }
            public string gpu_mdev { get; set; }
            public string gpu_vendor { get; set; }
            public string gpu_device { get; set; }
            public string gpu_power_draw { get; set; }
            public string gpu_power_limit { get; set; }
            public string gpu_memory_usage { get; set; }
            public string gpu_memory_total { get; set; }
            public string gpu_temperature { get; set; }
            public string gpu_fan { get; set; }
            public List<vm> gpu_vm { get; set; }
        }

        public class vm
        {
            public string vm_id { get; set; }
            public string vm_state { get; set; }
            public string vm_gpu_memory { get; set; }
        }
    }
}

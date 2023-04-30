__Used for monitoring CPU and GPU of a proxmox server from a windows computer__

*You need to run VM_Manager_Host service on the proxmox host __Do not set the proxmox web gui port in hosts.txt__*

Proxmox host ip's need to be specified in ``hosts.txt`` file, Each host needs to be on newline
---
This is an example:
```
10.0.0.201:6050
10.0.0.230:6050
```

Configuration file ``config.json`` is automatically created on first startup
---
This is the default config:
```
{
  "monitoring_refresh_rate_ms": 500,
  "cpu_max_temp": 90,
  "gpu_max_temp": 80,
  "color_background": "#282828",
  "color_bar_background": "#282828",
  "color_bar_border": "#AAAAAA",
  "color_cpu_usage": "#46C948",
  "color_ram_usage": "#468CC9",
  "color_cpu_temperature": "#FF8800",
  "color_gpu_power_draw": "#FF4A4A",
  "color_gpu_memory_usage": "#46C948",
  "color_gpu_temperature": "#FF8800",
  "color_gpu_fan": "#468CC9",
  "color_gpu_memory_bar_used": "#46C948",
  "color_gpu_memory_bar_free": "#468CC9"
}
```

*This is not the best code but works for me...

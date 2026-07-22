# Homelab Monitor

A Proxmox VE monitoring dashboard built with ASP.NET Core and Blazor.

## Overview
Homelab Monitor provides a real-time view of a Proxmox host's resources, including CPU, memory, storage, virtual machines, LXC containers, and activity logs - all retrieved through the Proxmox API.

## Tech Stack
- **Backend:** ASP.NET Core 10 Web API
- **Frontend:** Blazor (MudBlazor)
- **Database:** PostgreSQL with Entity Framework Core

## Features
- Dashboard with live node metrics
- Compute resources view (VMs and LXC containers)
- Storage pool overview
- Activity log with status tracking
- Settings for managing Proxmox host connections

## Screenshots

<figure align="center">
    <img src="screenshots/dashboard.png" alt="Dashboard" style="border: 1px solid black">
</figure>

<figure align="center">
    <img src="screenshots/compute.png" alt="Compute" style="border: 1px solid black">
</figure>

<figure align="center">
    <img src="screenshots/storage-1.png" alt="Storage-1" style="border: 1px solid black">
</figure>

<figure align="center">
    <img src="screenshots/storage-2.png" alt="Storage-2" style="border: 1px solid black">
</figure>

<figure align="center">
    <img src="screenshots/activity-logs.png" alt="Activity Logs" style="border: 1px solid black">
</figure>

<figure align="center">
    <img src="screenshots/settings.png" alt="Settings" style="border: 1px solid black">
</figure>
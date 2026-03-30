# HomeLabMonitor Context

This project is a centralized dashboard for monitoring home lab hardware and services.

## Technical Stack
- **Framework:** ASP.NET Core Blazor Server.
- **UI Library:** MudBlazor.
- **Styling:** Bootstrap 5.3.3 (used for layout utility classes).
- **Render Mode:** Interactive Server Render Mode.

## Coding Guidelines
- Always prefer MudBlazor components (e.g., `<MudGrid>`, `<MudCard>`, `<MudButton>`) over standard HTML elements.
- Ensure all new Razor components are placed within the `Web.Components` namespace.
- Follow standard dependency injection patterns for data services.
- Use the built-in Bootstrap utilities for quick margin/padding adjustments only when MudBlazor components lack the specific styling needed.
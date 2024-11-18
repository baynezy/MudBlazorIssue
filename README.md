# MudBlazor Issue

This is a reproduction case to demonstrate an issue with MudBlazor. Specifically, validation triggering correctly when
you are dynamically adding components to a form.

## Steps to reproduce

1. Clone this repository
2. Run the project
3. Open side navigation if it's not already open
4. Click on the _"Nested Component Issues"_ link
5. Click on Enabled
6. Click on _"Add Interval"_ button
7. Click on _"Create Heartbeat Setting"_ 
8. Note that inline validation only shows on the first interval, but not on the second interval
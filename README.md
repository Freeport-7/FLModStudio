# FLModStudio
Freelancer modding IDE with a powerful 3D system editor

# Getting Started

Freelancer Mod Studio is a well-polished and powerful IDE for both experienced as well as beginner modders. You can easily create and edit your mods by using the visual ini editor with unlimited undo/redo, powerful clipboard functionality, multi-editing and 3D model view support. The implemented 3D system editor shows Freelancer models and supports position,rotation and scale manipulation inside the editor.

Download and install the latest release [here.](https://github.com/Freeport-7/FLModStudio/releases)

In order to run this tool, you'll need .Net Framework 4.8

## Keybindings
| Action | Binding |
| ----------- | ----------- |
| Select nearest object	| Left mouse button |
| Select farthest object |	Left mouse button + SHIFT |
| Add item to current selection	| Hold CTRL while selecting nearest/farthest object |
| Rotate camera |	Hold right mouse button |
| Rotate camera (around point) |	Hold left mouse button + ALT |
| Zoom camera (smooth) |	Hold right mouse button + ALT |
| Zoom camera (fast) |	Scroll mouse-wheel |
| Pan camera |	Hold right mouse button + SHIFT |
| Look-at click point	| Double-click right mouse button |
| Reset camera	| Double-click right mouse button + SHIFT |
| Move camera forward |	W (faster: + SHIFT) |
| Move camera backward |	S (faster: + SHIFT) |
| Move camera left |	A (faster: + SHIFT) |
| Move camera right |	D (faster: + SHIFT) |
| Move camera up |	SPACE (faster: + SHIFT) |
| Move camera down |	E (faster: + SHIFT) |
| Move object up |	CAPSLOCK + W (single unit: + SHIFT) |
| Move object down |	CAPSLOCK + S (single unit: + SHIFT) |
| Move object left |	CAPSLOCK + A (single unit: + SHIFT) |
| Move object right |	CAPSLOCK + D (single unit: + SHIFT) |
| Focus selected object |	F |
| Look-at selected object |	SHIFT + F |
| Track selected object |	T |

## Known Issues

- Saving files with ini-entries which require custom order within the same ini-section are not yet supported. This includes the following files:
-- Mission Encounters (missions\encounters\*.ini)
-- Missions (missions\\m\.ini)
-- Solar Asteroids (solar\asteroids\*.ini)
-- Solar Nebulae (solar\nebula\*.ini)
    
- All ini sections or entries with invalid names will be ignored and therefore are not written on save. See the shipped Template.xml file for a complete list. However you can easily edit this file which is located in the same directory as the .exe to change this behavior.

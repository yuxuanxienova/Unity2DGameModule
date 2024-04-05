1. The Canvas containing the inventory screen and the equipment screen should be set as:

Render Mode: Screen Space -Camera
RenderCamera: MainCamera
SortingLayer: SuitableLayer

2. You need to add event trigger component to script that has UserInterface attach to it.

3. the element 0 of the database should be a default object with default type

4. the every slots in static interface should set at least one allowed item with allowed type "default" 

5. the background object in the EquipmentScreen should be the first child, following by the slots

6. The png image of all unit must be 1000pixel*1000pixel



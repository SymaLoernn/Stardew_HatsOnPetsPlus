# Stardew_HatsOnPetsPlus
An original Stardew Valley mod that aims to let modders correctly place hats on their custom pets

## Curent TODO list
- Let player add hats on pets other than dogs and cats (vanilla or modded), need to overwrite the condition on line 621 from the Pets.cs file 
- Add a way to set default sprite position for a pet/breed
- Clean up the code (remove test code, maybe move classes in subfolders, etc...)
- Figure out the update key thing from SMAPI
- Maybe add custom hat sprite data for turtles ?
- Make a better README to explain how to add sprite data with content patcher
- Make a Nexus Mod Page to make the mod public once it's complete

### JSON sprite data information (not final but shouldn't change much)

```
"YourName.YourModName": [
    {
      "Type": "Cat",  // Mandatory field, can be "Cat", "Dog" or any other pet type that can wear a hat
      "BreedId": "3",  // Mandatory field, this value with the cat Type refer to the white cat
      "Sprites": [
        {
          "SpriteId": 0,         // Mandatory int, represent the sprite number on the sprite sheet
          "HatOffsetX": 0.0,     // Optional float (default to 0), represent the X offset of the hat from the center of the sprite
          "HatOffsetY": 20.0,    // Optional float (default to 0), represent the Y offset of the hat from the center of the sprite
          "Direction": 2,        // Optional int (default to 2), change which hat sprite is drawn (0 if facing up, 1 is facing right, 2 is down and 3 is left)
          "Scale": 1.4,          // Optional float (default to 1.333...), change the size of the hat sprite
          "Flipped": false       // Optional bool (default to false), determine if this data is to be used when the pet sprite is flipped or not  (this one seems to mostly apply to sleeping/eating sprites)
        },
        { ... }  // Repeat the previous block for every sprite where you want a custom hat position/direction/scale
     ]
  },
 {...}  // Repeat everything above if your mod add/replace multiple pets/breeds
}
```
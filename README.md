# Stardew_HatsOnPetsPlus
A Stardew Valley framework that aims to let modders place hats on their custom pets

## Curent TODO list

- Add a way to set a default sprite position for a pet/breed
- Clean up the code (remove test code, maybe move classes in subfolders, etc...)
- Figure out the update key thing from SMAPI
- Make a better README to explain how to add sprite data with content patcher
- Make a Nexus Mod Page to make the mod public once it's complete (maybe contact an artist to make a banner)
- (Bonus) Provide a full ContentPatcher json example for vanilla cats and/or dogs (could be useful as a template)

Once everything above is done, maybe start a second project to make an online or downloadable tool for modders to make it easy to create custom hat data (for example by loading a sprite sheet and placing a hat on every frame) 

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

## Credits

Mod author : Syma  
Mod contributor : Elizabethcd  
For their help : Elizabethcd, the wonderful people of the Stardew Valley modding channel on the official Discord server  
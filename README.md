# Stardew_HatsOnPetsPlus
A Stardew Valley framework that aims to let modders place hats on their custom pets and resprites  

## Documentation for modders

In this documentation, I will assume that you are making a Content Patcher mod to either resprite a pet/breed or add a new pet/breed, and that you already were able to do it with Content Patcher.  
If you didn't do this part yet, I suggest that you do so before working on the hat placement, as the knowledge you'll gain by doing this will help you better understand this documentation.  

### 1. How to register my mod for Hats On Pets Plus

To configure your mod to use this framework, you will only need to edit the content.json file of your Content Patcher project, and add an EditData action that will look like this :  
```
{
"Action":"EditData",
"Target":"Syma.HatsOnPetsPlus/CustomPetData",
"Entries":{
    "<YourName.YourModName>": [
        { <First modded Pet/Breed data - See below for more information> },
        { <Second modded Pet/Breed data - See below for more information> },
        ...
    ]
}
```
<YourName.YourModName> should be an unique identifier. If for any reason you need to use multiple EditData actions, please use a different identifier for every use (ex : <YourName.YourModName.Option1>)  
The speficic data for each Pet/Breed is covered in the next section.

### 2. How to add custom hat data for a Pet/Breed

The data for drawing hats on your custom Pet and Breed is represented in the following json block : 

```
{
    "Type": <Type>,
    "BreedId": <BreedId>,
    "Sprites": [
        {
            "SpriteId": <SpriteId>,
            "HatOffsetX": <HatOffSetX>,
            "HatOffsetY": <HatOffSetY>,
            "Direction": <Direction>,
            "Scale": <Scale>,
            "Flipped": <Flipped>,
            "Default": <Default>,
            "DoNotDrawHat": <DoNotDrawHat>
        },
        { ... }  // Repeat the previous block for every sprite where you want a custom hat position/direction/scale
    ]
}
```
Signification of each field : 

| Field Name | Field Type | Optional | Default value | Description                                          |
| :------------------ | :------: | :----: | ----: | :---- |
| Type       | String | **No** | - | The pet Type, can be "Cat", "Dog" (case sensitive) or any other vanilla or custom pet type |
| BreedId       | String | **No** | - | The breed ID of the pet, for vanilla pets it a number in a string (ex : "2") |
| **For each sprite :** | | | | |
| SpriteId       | Int | **No** | - | The sprite number on the sprite sheet (it starts from 0 on the upper left corner and goes left to right then up to down) |
| HatOffSetX       | Float | Yes | 0 | The X offset of the hat from the center of the sprite, can be negative |
| HatOffSetY       | Float | Yes | 0 | The X offset of the hat from the center of the sprite, can be negative |
| Direction       | Int | Yes | 2 | Choose which hat sprite is drawn (0 if facing up, 1 is facing right, 2 is down and 3 is left), values stay in the 0 to 3 range to avoid issues |
| Scale       | Float | Yes | 1.333 | The size of the hat sprite, should be positive to avoid weird things |
| Flipped       | Bool | Yes | false | If set to true, the data in this block will only be used for the flipped version of the assets (seems to mostly apply to sleeping/eating sprites) |
| Default       | Bool | Yes | false | If set to true, use the data in this block as default on any sprite with no data on this pet/breed, only one default is allowed per pet and breed (multiple will get overwritten) |
| DoNotDrawHat       | Bool | Yes | false | If set to true, do not attempt to draw the hat for this sprite (and ignore the hatOffsetX, hatOffsetY, Direction and Scale values) |

### 3. How to determine the correct values for my sprites ?

TODO - needs examples, tips and anything that can help  
Some helpful data : 
- X goes left to right  
- Y goes up to bottom
- The X and Y origins (X = 0 and Y = 0) are at the center of the sprite  
- To move 1 pixel on the spritesheet, you need to add/substract 4 to the X or Y value  (might have to do with the zoom scale)  

## Credits

Mod author : Syma  
Mod contributor : Elizabethcd  
For their help : Elizabethcd, the wonderful people of the Stardew Valley modding channel on the official Discord server

## Current and future TODO list

- Complete the documentation on the README
- Add examples of jsons in the documentation/mod folders
- (Bonus) Contact an artist or make a banner myself for the Nexus page mod
- (Bonus) Provide a full ContentPatcher json example for vanilla cats and/or dogs (could be useful as a template)

Once everything above is done, maybe start a second project to make an online or downloadable tool for modders to make it easy to create custom hat data (for example by loading a sprite sheet and placing a hat on every frame) 

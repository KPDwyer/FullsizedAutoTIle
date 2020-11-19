# How to use RPGMaker AutoTile package

usage.md contributed by Ivan - thanks Ivan!

## About

In this article we won’t be going in-depth on what exactly is an RPGMaker AutoTile package since it’s creator has a detailed article about this right [here](https://kpdwyer.github.io/unity/2019/04/18/Autotile-Pipeline.html#in-closing).

Instead, here you will find simple instructions on how to get this amazing package, how to import it in your Unity project correctly and how to use it.

Also, it’s important to know that this guide is about downloading and setting up a [Full-sized Tiles implementation](https://kpdwyer.github.io/unity/2019/04/18/Autotile-Pipeline.html#implementation-1-full-sized-tiles) of AutoTile. And it’s based on using 32x32 tilesets. You can start with this, get familiar with how it works and later optimize and change everything the way you need.

## How to download the package

You won’t be able to get this package via Unity Package Manager. The reason is: Unity won't allow a custom package's dependency list to include a non-registry package. And since RPGMaker AutoTile is based on 2D-Extras package functionality and 2d-extras is not officially in the registry, it’s impossible to set up AutoTile as a downloadable Unity package. Instead, it’s stored as a Unity Project.

That’s why, to download it correctly, you can do one of two things:

- Clone it from it’s GitHub page:
  - Instructions for Github Desktop:
    - Install GitHub Desktop;
    - Go to AutoTile GitHub page;
    - Go to green “Code” button and click on it;
    - Click on “Open with GitHub Desktop” option;
    - Clone it to your hard drive somewhere.

- Download it as a ZIP archive:
  - Go to AutoTile GitHub page;
  - Go to green “Code” button and click on it;
  - Click on “Download ZIP” option;
  - Unpack the downloaded ZIP somewhere.

Either way you’ll get an unpacked Unity project with AutoTile as a result.

## How to import it to your Unity project

You need to copy the AutoTile folder with all its contents. It should be located here:

“*~\FullsizedAutoTilePipe\Fullsized AutoTile\Assets\AutoTile*”

After you’ve copied the AutoTile folder, go to the Unity project you’d like to import it to and just paste it somewhere.

Yep, it’s that easy.

## How to start using it

There are several easy steps you need to make:

1. Get a tileset you want to turn into a RuleTile brush. To make your first experience simpler, just make sure that a single tile is 32x32 pixels and tiles on this tileset are organized just as they are in the examples in [this](https://kpdwyer.github.io/unity/2019/04/18/Autotile-Pipeline.html#overview--quick-start) article.

2. Open your Unity project;

3. Copy your preferred tileset in your project somewhere. Also it’s important to name this file the way you’d like to name your resulting RuleTile brush (names can be changed later though);

4. Click on your tileset and go to the Unity Inspector Window. Then, set the following settings:

   - `Pixels Per Unit: 32`

   - Advanced ➤ `Read/Write Enabled: check`
   - `Filter Mode: Point (no filter)` - this is optional, but will make your pixel art more crisp.

5. Now go to your AutoTile folder, here you will find a Scriptable Object named “RPGMaker_A2”. Click on it and go to the Unity Inspector Window:

   - In Unity Inspector Window you will see a field that accepts 2D Textures. You need to place here your preferred tileset texture (make sure it’s set correctly, see step 4). You can either just drag it here from another folder or use Unity interface (circle at the right end of the field) and pick it from the list of available textures. Do so.

8. Now you just need to wait while the magic of RuleTile creation is performed :)

9. After it’s done, you will find new folders in your project. First, in your root (Assets) there will be a new “Autotiles” folder and inside - another one, named just like your tileset file. Inside you will find a lot of individual sprites with tiles and, what’s more important, a RuleTile brush, set up and ready to be used.

10. Now you just need to add this RuleTile brush to any Tile Palette you like and start using it. Enjoy! :)
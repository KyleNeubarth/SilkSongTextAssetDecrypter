# Silksong Text Asset Decrypter

usage: dotnet run <input-dir> <output-dir>

Simple project for decrypting SilkSong text sheets. Takes all files in the input directory and runs them through the decryption function, then writes them to the provided output directory.

I recommend using AssetRipper to extract encrypted sheets from the SilkSong resources.assets assetbundle. `EncryptedSheets_9-22-2025` contains all the english text sheets I extracted from the bundle this way. These sheets are liable to change as updates and DLC come out though, so I don't recommend using them in the far future.
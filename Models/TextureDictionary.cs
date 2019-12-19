using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System;
using System.Threading.Tasks;

namespace gghud.Models
{
    public struct TextureDictionary
    {
        public string Name { get; }
        public bool IsLoaded => HasStreamedTextureDictLoaded(Name);

        public TextureDictionary(string name)
        {
            Name = name;
        }

        public void Dismiss()
        {
            SetStreamedTextureDictAsNoLongerNeeded(Name);
        }

        public void Load()
        {
            RequestStreamedTextureDict(Name, true);
        }

        public async Task LoadAndWait()
        {
            Load();

            var startTime = GetGameTimer();

            while (!IsLoaded && GetGameTimer() - startTime < 5000)
            {
                await BaseScript.Delay(0);
            }

            await Task.FromResult(0);
        }

        public TextureReference[] GetTextures()
        {
            TextureReference[] result = new TextureReference[0];

            return result;
        }
        public static implicit operator TextureDictionary(string name) => new TextureDictionary(name);
        public static implicit operator string(TextureDictionary textureDictionary) => textureDictionary.Name;
    }
}
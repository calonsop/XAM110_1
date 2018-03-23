using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using System.Reflection;

namespace MyTunes
{
	public static class SongLoader
	{
		const string Filename = "songs.json";

		public static async Task<IEnumerable<Song>> Load()
		{
			using (var reader = new StreamReader(await OpenData())) {
				return JsonConvert.DeserializeObject<List<Song>>(await reader.ReadToEndAsync());
			}
		}

		private static async Task<Stream> OpenData()
        {
            await Task.Yield();
#if __IOS__
            return System.IO.File.OpenRead(Filename);
#endif

#if __ANDROID__
            return Android.App.Application.Context.Assets.Open(Filename);
#endif

#if WINDOWS_UWP
            var file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(Filename);
            return await file.OpenStreamForReadAsync();

#else
            // TODO: add code to open file here.
            return null;
#endif
        }
    }
}


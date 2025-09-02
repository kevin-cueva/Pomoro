namespace Pomoro.Services;

using Plugin.Maui.Audio;
using Pomoro.Services.Interfaces;

public class PlaySoundEndPomodoro(
    IAudioManager audioManager
): IPlaySoundEndPomodoro
{
    private readonly IAudioManager AudioManager = audioManager;

    public async Task ReproducirSonidoFinPomodoro()
    {
        var player = AudioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("timer-terminer.mp3"));
        player.Play();
    }
}
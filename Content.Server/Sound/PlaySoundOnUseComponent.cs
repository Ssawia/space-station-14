using Content.Shared.Interaction.Events;
using Robust.Shared.Audio.Systems;
using Content.Server.Chat.Systems;
using Content.Server.NPC.Systems;
using Robust.Shared.Serialization.Manager;
using Content.Server.Pinpointer;
namespace Content.Server.Sound;
using Robust.Shared.Player;



[RegisterComponent]
public sealed partial class PlaySoundOnUseComponent : Component
{
    [DataField]
    public string Sound = string.Empty;

    [DataField]
    public string Message = string.Empty;

}

public sealed class PlaySoundOnUseSystem : EntitySystem
{

    [Dependency] private readonly SharedAudioSystem _audio = default!;

    [Dependency] private readonly ChatSystem _chat = default!;
    [Dependency] private readonly NavMapSystem _navMap = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<PlaySoundOnUseComponent, UseInHandEvent>(OnUseInHand);
    }

    private void OnUseInHand(Entity<PlaySoundOnUseComponent> ent, ref UseInHandEvent args)
    {
        _audio.PlayPvs(ent.Comp.Sound, ent.Owner);


        var msg = ent.Comp.Message;


        _chat.DispatchGlobalAnnouncement(msg, playSound: false, colorOverride: Color.Red);
        _audio.PlayGlobal("/Audio/Misc/notice1.ogg", Filter.Broadcast(), true);
        //_navMap.SetBeaconEnabled(uid, true);
    }
}

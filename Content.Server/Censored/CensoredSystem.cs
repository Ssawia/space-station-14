using System.Linq;
using Content.Shared;
using Content.Shared.Weapons.Melee;
using Content.Shared.Mobs;
using Content.Shared.Damage;
using Content.Shared.Mobs.Components;
using Content.Shared.Body.Components;
using Content.Shared.Weapons.Melee.Events;
using Content.Server.Body.Systems;
using Content.Server.Chat.Systems;
using Content.Shared.Censored;
using Robust.Shared.Audio.Systems;


namespace Content.Server.Censored;

public sealed class CensoredSystem : EntitySystem
{

    [Dependency] private readonly SharedAudioSystem _audio = default!;
    [Dependency] private readonly DamageableSystem _damageable = default!;
    [Dependency] private readonly ChatSystem _chat = default!;
    [Dependency] private readonly BodySystem _body = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<CensoredComponent, MeleeHitEvent>(OnMeleeHit);
    }

    private void OnMeleeHit(EntityUid uid, CensoredComponent comp, MeleeHitEvent args)
    {
        if (!TryComp<CensoredComponent>(args.User, out _))
            return;

        if (!args.HitEntities.Any())
            return;

        foreach (var entity in args.HitEntities)
        {
            if (args.User == entity)
                continue;

            if (!TryComp<MobStateComponent>(entity, out var mobState))
                continue;

            if (!TryComp<DamageableComponent>(entity, out var dmgComp))
                continue;

            if (!TryComp<MeleeWeaponComponent>(entity, out var meleeComp))
                continue;

            if (!TryComp<BodyComponent>(entity, out var bodyComp))
                continue;




            //var modifiedDamage = DamageSpecifier.ApplyModifierSets(damage + hitEvent.BonusDamage + attackedEvent.BonusDamage, hitEvent.ModifiersList);

            //_meleeSound.PlayHitSound(entity, uid, GetHighestDamageSound(modifiedDamage, _protoManager), hitEvent.HitSoundOverride, component);





            var delta = _damageable.GetDamage(entity, comp.DamageOnBite, false, false);
            if (delta != null)
            {
                var possibleDmg = dmgComp.TotalDamage + delta.GetTotal();

                if (possibleDmg >= 100 || mobState.CurrentState == MobState.Critical)
                {
                    _audio.PlayPvs(comp.ConvertAudio, uid);
                    _chat.DispatchGlobalAnnouncement(comp.Message, playSound: false, colorOverride: Color.Red);

                    var children = Spawn(comp.ArmyMobSpawnId, Transform(entity).Coordinates);
                    _body.GibBody(entity,true);



                    var compServ = EnsureComp<CensoredChildrenComponent>(children);
                    //compServ.Father = uid;
                    //Dirty(children, comp);

                    comp.Children.Add(children);

                }
                else
                {
                    _chat.DispatchGlobalAnnouncement("Get not [CENSORED]: " + possibleDmg, playSound: false, colorOverride: Color.Red);
                }
            }




        }

    }

}

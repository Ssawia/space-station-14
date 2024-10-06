using Content.Shared.Damage;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;

namespace Content.Server.Censored;


[RegisterComponent]
public sealed partial class CensoredComponent : Component
{
    [DataField]
    public string Message = string.Empty;

    [DataField]
    public string ConvertAudio = string.Empty;

    [DataField]
    public string AttackAudio = string.Empty;

    [DataField]
    public bool IgnoreResistances = true;

    [DataField("damageOnBite")]
    public DamageSpecifier DamageOnBite = new()
    {
        DamageDict = new()
        {
            { "Piercing", 26 }
        }
    };

    [DataField("children")]
    public HashSet<EntityUid> Children = new();

    [ViewVariables(VVAccess.ReadWrite), DataField("armyMobSpawnId", customTypeSerializer: typeof(PrototypeIdSerializer<EntityPrototype>))]
    public string ArmyMobSpawnId = "MobCensoredChildren";
}






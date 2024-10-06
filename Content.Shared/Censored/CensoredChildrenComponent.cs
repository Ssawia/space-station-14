namespace Content.Shared.Censored;
using Robust.Shared.GameStates;

[RegisterComponent, NetworkedComponent, Access(typeof(SharedCensoredSystem))]
[AutoGenerateComponentState]
public sealed partial class CensoredChildrenComponent : Component
{
    /// <summary>
    /// The father this censored belongs to.
    /// </summary>
    [DataField("father")]
    [AutoNetworkedField]
    public EntityUid? Father;
}

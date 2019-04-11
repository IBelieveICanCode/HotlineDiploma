public interface IDestructable
{
    float MaxHealth { get; }
    float CurrentHealth { get; set; }
    void ReceiveHit(float damage);
}
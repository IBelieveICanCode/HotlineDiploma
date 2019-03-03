public interface IDestructable
{
    float Health { get; set; }
    void ReceiveHit(float damage);
    void Die();
}
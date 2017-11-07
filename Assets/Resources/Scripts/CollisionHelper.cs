using UnityEngine;

public static class CollisionHelper
{
    public static bool isCollisionFromBellow(Transform player, Transform collider)
    {
		float y;
		return DidCollideWithHorizontalEdges(player, collider, out y) && y < 0.0f;
    }

    public static bool isCollisionFromAbove(Transform player, Transform collider)
    {
		float y;
		return DidCollideWithHorizontalEdges(player, collider, out y) && y > 0.0f;   
    }

	private static bool DidCollideWithHorizontalEdges(Transform player, Transform collider, out float yPos)
	{
		var hit = Physics2D.Raycast(
            player.position, 
            player.position.y > collider.position.y ? Vector2.down : Vector2.up, 
            0.1f);
		var relativeHitPosition = collider.InverseTransformPoint(hit.point);
		yPos = relativeHitPosition.y;

		return relativeHitPosition.x > -0.49f && relativeHitPosition.x < 0.49f;
	}
}
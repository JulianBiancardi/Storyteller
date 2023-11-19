using UnityEngine;


public class Expression : MonoBehaviour
{
    public SpriteRenderer backGroundSprite;
    public SpriteRenderer contentSprite;


    public void Instanciate(Sprite backGround, Sprite content){
        backGroundSprite.sprite = backGround;
        contentSprite.sprite = content;
    }
}

using ReadyPlayerMe.Core;
using UnityEngine;

public class AvatarLoader : MonoBehaviour
{
    [SerializeField] [Tooltip("Set this to the URL or shortcode of the Ready Player Me Avatar you want to load.")]
    private string avatarUrl = "https://models.readyplayer.me/66e1fea3822e77ab711ca295.glb";


    [SerializeField] private GameObject avatar;

    private GameObject tempAvatar;

    private void Start()
    {
        ApplicationData.Log();
        var avatarLoader = new AvatarObjectLoader();
        // use the OnCompleted event to set the avatar and setup animator
        avatarLoader.OnCompleted += (_, args) =>
        {
            tempAvatar = args.Avatar;
            AvatarMeshHelper.TransferMesh(tempAvatar, avatar);
            Destroy(tempAvatar);
        };
        avatarLoader.LoadAvatar(avatarUrl);
    } 
}

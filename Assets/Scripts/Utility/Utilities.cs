using UnityEngine;

static class Tags {
  public static string Player = "Player";
  public static string Ground = "Ground";
  public static string Canvas = "Canvas";
  public static string Grabbable = "Grabbable";
  public static string Untagged = "Untagged";

}

static class Layers {
  public static int Player = LayerMask.NameToLayer("Player");
  public static int Documents = LayerMask.NameToLayer("Documents");
}

///<summary>
/// To be used with the Input.GetKey methods.
///</summary>
static class KeyMappings {
  public static string Interact = "Fire3";
  public static string Jump = "Jump";
  public static KeyCode DropDocument = KeyCode.Q;
}
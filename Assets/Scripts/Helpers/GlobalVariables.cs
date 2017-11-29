using UnityEngine;

static class Tags {
  public static string Player = "Player";
  public static string Ground = "Ground";
  public static string Canvas = "Canvas";

}

static class Layers {
  public static int Player = LayerMask.NameToLayer("Player");
}

///<summary>
/// To be used with the Input.GetKey methods.
///</summary>
static class KeyMappings {
  public static string Interact = "Fire3";
  public static string Jump = "Jump";
}
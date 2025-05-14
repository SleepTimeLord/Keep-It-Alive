using UnityEngine;

public interface ITriggerCommandable
{
    // what the command is
    string command {  get; set; }

    // fuct to change the command
    void ActionCommanded (string action);
}

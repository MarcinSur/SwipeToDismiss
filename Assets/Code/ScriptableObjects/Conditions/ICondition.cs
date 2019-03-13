using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICondition  {

    string Desription { get; set; }
    bool Satisfied { get; set; }
    int Hash { get; set; }
}
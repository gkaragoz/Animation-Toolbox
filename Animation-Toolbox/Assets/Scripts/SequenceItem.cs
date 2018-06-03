using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceItem : Toolbox {
    public override event ClickEventHandler OnClicked;

    public override IEnumerator Play() {
        throw new System.NotImplementedException();
    }

    public override IEnumerator PlayAfterAWhile(float delay) {
        throw new System.NotImplementedException();
    }

    public override void Stop() {
        throw new System.NotImplementedException();
    }
}

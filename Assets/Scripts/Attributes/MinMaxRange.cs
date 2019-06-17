using System;

public class MinMaxRange : Attribute {

    public float min { get; private set; }
    public float max { get; private set; }

    public MinMaxRange(float min, float max) {
        this.min = min;
        this.max = max;
    }
}

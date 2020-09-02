internal class Layer {
    private const string PLAYER = "Player";

    public void teste() {
        string layerA = "Player";
        if (layerA.Length != PLAYER.Length) {
            return;
        }

        char[] layerAChar = layerA.ToCharArray();
        char[] playerChar = PLAYER.ToCharArray();

        for (int index = 0; index < layerA.Length; index++) {
            if (layerAChar[index] != playerChar[index]) {
                return;
            }
        }

        //é igual
    }

    public void teste2() {
        int a = 0b0000_0000_0000_0000_0000_0000_1000_0000;
        int b = 0b0000_0000_0000_0000_0000_0001_0000_0000;

        int c = -a; // 0b0000_0000_0000_0000_0000_0000_1000_0000
        // 0b1111_1111_1111_1111_1111_1111_0111_1111 + 1
        // 0b1111_1111_1111_1111_1111_1111_1000_0000

        int c1 = ~a + 1;

        int d = ~a; // 0b1111_1111_1111_1111_1111_1111_0111_1111

        int e = a & b; // 0b0000_0000_0000_0000_0000_0000_1000_0000
        // 0b0000_0000_0000_0000_0000_0001_0000_0000
        // 0b0000_0000_0000_0000_0000_0000_0000_0000

        int f = a | b; // 0b0000_0000_0000_0000_0000_0000_1000_0000
        // 0b0000_0000_0000_0000_0000_0001_0000_0000
        // 0b0000_0000_0000_0000_0000_0001_1000_0000


        int player = 0b0000_0000_0000_0000_0000_0001_0000_0000; // int player = 1 << 8;
        int ground = 0b0000_0000_0000_0000_0000_0010_0000_0000; // int ground = 1 << 9;

        int objLayer = 0b0000_0000_0000_0000_0000_0001_0000_0000;
        int hit =      0b0000_0000_0000_0000_0000_0011_0000_0000;
        bool collide = (objLayer & hit) != 0;

        int g = a << 1; // Leva todos os bits uma casa para esquerda

        int h = a >> 1; // Leva todos os bits uma casa para direita
    }
}
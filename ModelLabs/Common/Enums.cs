using System;

namespace FTN.Common
{	
	public enum PhaseCode : short
	{
		Unknown = 0x0,
		N = 0x1,
		C = 0x2,
		CN = 0x3,
		B = 0x4,
		BN = 0x5,
		BC = 0x6,
		BCN = 0x7,
		A = 0x8,
		AN = 0x9,
		AC = 0xA,
		ACN = 0xB,
		AB = 0xC,
		ABN = 0xD,
		ABC = 0xE,
		ABCN = 0xF
	}

    public enum UnitMultiplier : short
    {
        c = -2,
        d = -1,
        G = 9,
        k = 3,
        m = -3,
        M = 6,
        micro = -6,
        n = -9,
        none = 0,
        p = -12,
        T = 12
    }

    public enum UnitSymbol : short
    {
        Unknown = 0x0,
        A = 0x1,
        deg = 0x2,
        degC = 0x3,
        F = 0x4,
        g = 0x5,
        h = 0x6,
        H = 0x7,
        Hz = 0x8,
        J = 0x9,
        m = 0xA,
        m2 = 0xB,
        m3 = 0xC,
        min = 0xD,
        N = 0xE,
        none = 0xF,
        ohm = 0x10,
        Pa = 0x11,
        rad = 0x12,
        s = 0x13,
        S = 0x14,
        V = 0x15,
        VA = 0x16,
        VAh = 0x17,
        VAr = 0x18,
        VArh = 0x19,
        W = 0x1A,
        Wh = 0x1B
    }

    public enum RegulatingControlModeKind : short
    {
        voltage = 0,
        activePower = 1,
        reactivePower = 2,
        currentFlow = 3,
        @fixed = 4,
        admittance = 5,
        timeScheduled = 6,
        temperature = 7,
        powerFactor = 8
    }			
}

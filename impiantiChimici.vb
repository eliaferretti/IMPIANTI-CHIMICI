'IMPIANTI CHIMICI'

Function Rachford_Rice(z As Variant, p0 As Variant, p As Double, beta As Double, NC As Integer) As Double
Dim k(1000) As Variant
Dim i As Integer

    For i = 1 To NC
    k(i) = p0(i) / p
    Next
    
    Rachford_Rice = 0
    For i = 1 To NC
        Rachford_Rice = Rachford_Rice + z(i) * (k(i) - 1) / (1 + beta * (k(i) - 1))
    Next
End Function

Function Flash_pRR(z As Variant, p0 As Variant, p As Double, RR As Double, specie As Integer, fase As String, NC As Integer) As Double
Dim k(1000) As Variant
Dim i As Integer

    If (fase = "V") Then
        RRV = RR
    Else
        RRV = 1 - RR
    End If
    
    For i = 1 To NC
        k(i) = p0(i) / p
    Next
    
    Flash_pRR = 0
    
    For i = 1 To NC
        Flash_pRR = Flash_pRR + z(i) * (k(i) - 1) / (k(specie) + RRV * (k(i) - k(specie)))
    Next
End Function

Function Antoine(a As Double, b As Double, c As Double, T_inK As Double, baseLog As Double, unitaPressioneFormula As String, unitaTemperaturaFormula As String, unitaPressioneDesiderata As String) As Double
Dim T As Double
    If unitaTemperaturaFormula = "K" Then
        T = T_inK
    ElseIf unitaTemperaturaFormula = "°C" Then
        T = T_inK - 273.15
    End If
    
    logP = a - b / (c + T)
    p = Exp(Log(baseLog) * logP)
    
    If unitaPressioneFormula = "Pa" Then
        p = p
    ElseIf unitaPressioneFormula = "bar" Then
        p = p * 100000
    ElseIf unitaPressioneFormula = "atm" Then
        p = p * 101325
    ElseIf unitaPressioneFormula = "mmHg" Then
        p = p * 133.322368
    End If
    
    If unitaPressioneDesiderata = "Pa" Then
        Antoine = p
    ElseIf unitaPressioneDesiderata = "bar" Then
        Antoine = p / 100000
    ElseIf unitaPressioneDesiderata = "atm" Then
        Antoine = p / 101325
    ElseIf unitaPressioneDesiderata = "mmHg" Then
        Antoine = p / 133.322368
    End If
End Function

Function N_stadi(q As Double, xD As Double, xB As Double, zF As Double, alfa As Double, k_rappRecupero As Double) As Integer
Dim beta As Double
Dim gamma As Double
Dim delta As Double
Dim a As Double
Dim b As Double
Dim c As Double
Dim sol_1 As Double
Dim sol_2 As Double
Dim xF As Double
Dim yF As Double
Dim x_sgn As Double
Dim y_sgn As Double
Dim x(1000) As Variant
Dim y(1000) As Variant
Dim check As Integer
Dim i As Integer
Dim intercetta As Double
Dim stadi_superiori As Integer
Dim stadi_inferiori As Integer

    If q <> 1 And q <> 0 Then
        beta = q / (q - 1)
        gamma = zF / (q - 1)
        
        a = beta * (alfa - 1)
        b = beta - alfa - gamma * (alfa - 1)
        c = -gamma
        
        delta = (b) ^ (2) - 4 * a * c
        sol_1 = (-b + (delta) ^ (0.5)) / (2 * a)
        sol_2 = (-b - (delta) ^ (0.5)) / (2 * a)
        If sol_1 > 0 And sol_1 < 1 Then
            x_sgn = sol_1
        ElseIf sol_2 > 0 And sol_2 < 1 Then
            x_sgn = sol_2
        End If
        
        y_sgn = beta * x_sgn - gamma
        m = (xD - y_sgn) / (xD - x_sgn)
        R_min = m / (1 - m)
        R = k_rappRecupero * R_min
        xF = (gamma + xD / (R + 1)) / (beta - R / (R + 1))
        yF = beta * xF - gamma
    ElseIf q = 1 Then
        x_sgn = zF
        y_sgn = alfa * x_sgn / (1 + x_sgn * (alfa - 1))
        m = (xD - y_sgn) / (xD - x_sgn)
        R_min = m / (1 - m)
        R = k_rappRecupero * R_min
        
        xF = x_sgn
        yF = R / (R + 1) * xF + xD / (R + 1)
    ElseIf q = 0 Then
        y_sgn = zF
        x_sgn = y_sgn / (alfa + y_sgn * (1 - alfa))
        m = (xD - y_sgn) / (xD - x_sgn)
        R_min = m / (1 - m)
        R = k_rappRecupero * R_min
        yF = y_sgn
        xF = (R + 1) / R * yF - xD / R
    End If

    y(1) = xD
    check = 1
    i = 0
    
    While (check = 1)
        i = i + 1
        x(i) = y(i) / (alfa - y(i) * (alfa - 1))
        y(i + 1) = x(i) * R / (R + 1) + xD / (R + 1)
        If x(i) < xF Then
            check = 0
        End If
    Wend
    stadi_superiori = i
    m = (xB - yF) / (xB - xF)
    intercetta = -m * xF + yF
    check = 1
    While (check = 1)
        y(i + 1) = m * x(i) + intercetta
        x(i + 1) = y(i + 1) / (alfa - y(i + 1) * (alfa - 1))
        If x(i + 1) < xB Then
            check = 0
        End If
        i = i + 1
    Wend
    N_stadi = i
    stadi_inferiori = N_stadi - stadi_superiori
End Function

Function N_stadiInf(q As Double, xD As Double, xB As Double, zF As Double, alfa As Double, k_rappRecupero As Double) As Integer
Dim beta As Double
Dim gamma As Double
Dim delta As Double
Dim a As Double
Dim b As Double
Dim c As Double
Dim sol_1 As Double
Dim sol_2 As Double
Dim xF As Double
Dim yF As Double
Dim x_sgn As Double
Dim y_sgn As Double
Dim x(1000) As Variant
Dim y(1000) As Variant
Dim check As Integer
Dim i As Integer
Dim intercetta As Double
Dim stadi_superiori As Integer
Dim stadi_inferiori As Integer
Dim N_stadi As Integer

    If q <> 1 And q <> 0 Then
        beta = q / (q - 1)
        gamma = zF / (q - 1)
        
        a = beta * (alfa - 1)
        b = beta - alfa - gamma * (alfa - 1)
        c = -gamma
        
        delta = (b) ^ (2) - 4 * a * c
        sol_1 = (-b + (delta) ^ (0.5)) / (2 * a)
        sol_2 = (-b - (delta) ^ (0.5)) / (2 * a)
        If sol_1 > 0 And sol_1 < 1 Then
            x_sgn = sol_1
        ElseIf sol_2 > 0 And sol_2 < 1 Then
            x_sgn = sol_2
        End If
        
        y_sgn = beta * x_sgn - gamma
        m = (xD - y_sgn) / (xD - x_sgn)
        R_min = m / (1 - m)
        R = k_rappRecupero * R_min
        
        xF = (gamma + xD / (R + 1)) / (beta - R / (R + 1))
        yF = beta * xF - gamma
        
        
    ElseIf q = 1 Then
        x_sgn = zF
        y_sgn = alfa * x_sgn / (1 + x_sgn * (alfa - 1))
        m = (xD - y_sgn) / (xD - x_sgn)
        R_min = m / (1 - m)
        R = k_rappRecupero * R_min
        
        xF = x_sgn
        yF = R / (R + 1) * xF + xD / (R + 1)
    ElseIf q = 0 Then
        y_sgn = zF
        x_sgn = y_sgn / (alfa + y_sgn * (1 - alfa))
        m = (xD - y_sgn) / (xD - x_sgn)
        R_min = m / (1 - m)
        R = k_rappRecupero * R_min
        yF = y_sgn
        xF = (R + 1) / R * yF - xD / R
    End If

    y(1) = xD
    check = 1
    i = 0
    
    While (check = 1)
        i = i + 1
        x(i) = y(i) / (alfa - y(i) * (alfa - 1))
        y(i + 1) = x(i) * R / (R + 1) + xD / (R + 1)
        If x(i) < xF Then
            check = 0
        End If
    Wend
    stadi_superiori = i
    m = (xB - yF) / (xB - xF)
    intercetta = -m * xF + yF
    check = 1
    While (check = 1)
        y(i + 1) = m * x(i) + intercetta
        x(i + 1) = y(i + 1) / (alfa - y(i + 1) * (alfa - 1))
        If x(i + 1) < xB Then
            check = 0
        End If
        i = i + 1
    Wend
    N_stadi = i
    N_stadiInf = N_stadi - stadi_superiori
End Function

Function N_stadiSup(q As Double, xD As Double, xB As Double, zF As Double, alfa As Double, k_rappRecupero As Double) As Integer
Dim beta As Double
Dim gamma As Double
Dim delta As Double
Dim a As Double
Dim b As Double
Dim c As Double
Dim sol_1 As Double
Dim sol_2 As Double
Dim xF As Double
Dim yF As Double
Dim x_sgn As Double
Dim y_sgn As Double
Dim x(1000) As Variant
Dim y(1000) As Variant
Dim check As Integer
Dim i As Integer
Dim intercetta As Double
Dim stadi_superiori As Integer
Dim stadi_inferiori As Integer

If q <> 1 And q <> 0 Then
    beta = q / (q - 1)
    gamma = zF / (q - 1)
    
    a = beta * (alfa - 1)
    b = beta - alfa - gamma * (alfa - 1)
    c = -gamma
    
    delta = (b) ^ (2) - 4 * a * c
    sol_1 = (-b + (delta) ^ (0.5)) / (2 * a)
    sol_2 = (-b - (delta) ^ (0.5)) / (2 * a)
    If sol_1 > 0 And sol_1 < 1 Then
        x_sgn = sol_1
    ElseIf sol_2 > 0 And sol_2 < 1 Then
        x_sgn = sol_2
    End If
    
    y_sgn = beta * x_sgn - gamma
    m = (xD - y_sgn) / (xD - x_sgn)
    R_min = m / (1 - m)
    R = k_rappRecupero * R_min
    
    xF = (gamma + xD / (R + 1)) / (beta - R / (R + 1))
    yF = beta * xF - gamma
    
ElseIf q = 1 Then
    x_sgn = zF
    y_sgn = alfa * x_sgn / (1 + x_sgn * (alfa - 1))
    m = (xD - y_sgn) / (xD - x_sgn)
    R_min = m / (1 - m)
    R = k_rappRecupero * R_min
    
    xF = x_sgn
    yF = R / (R + 1) * xF + xD / (R + 1)
ElseIf q = 0 Then
        y_sgn = zF
        x_sgn = y_sgn / (alfa + y_sgn * (1 - alfa))
        m = (xD - y_sgn) / (xD - x_sgn)
        R_min = m / (1 - m)
        R = k_rappRecupero * R_min
        yF = y_sgn
        xF = (R + 1) / R * yF - xD / R
End If
    y(1) = xD
    check = 1
    i = 0
    
    While (check = 1)
        i = i + 1
        x(i) = y(i) / (alfa - y(i) * (alfa - 1))
        y(i + 1) = x(i) * R / (R + 1) + xD / (R + 1)
        If x(i) < xF Then
            check = 0
        End If
    Wend
    N_stadiSup = i
End Function

Function Rmin(q As Double, xD As Double, zF As Double, alfa As Double) As Double
Dim beta As Double
Dim gamma As Double
Dim delta As Double
Dim a As Double
Dim b As Double
Dim c As Double
Dim sol_1 As Double
Dim sol_2 As Double
Dim x_sgn As Double
Dim y_sgn As Double
Dim check As Integer
Dim i As Integer
Dim intercetta As Double

If q <> 1 And q <> 0 Then
    beta = q / (q - 1)
    gamma = zF / (q - 1)
    
    a = beta * (alfa - 1)
    b = beta - alfa - gamma * (alfa - 1)
    c = -gamma
    
    delta = (b) ^ (2) - 4 * a * c
    sol_1 = (-b + (delta) ^ (0.5)) / (2 * a)
    sol_2 = (-b - (delta) ^ (0.5)) / (2 * a)
    If sol_1 > 0 And sol_1 < 1 Then
        x_sgn = sol_1
    ElseIf sol_2 > 0 And sol_2 < 1 Then
        x_sgn = sol_2
    End If
    
    y_sgn = beta * x_sgn - gamma
    m = (xD - y_sgn) / (xD - x_sgn)
    Rmin = m / (1 - m)
ElseIf q = 1 Then
    x_sgn = zF
    y_sgn = alfa * x_sgn / (1 + x_sgn * (alfa - 1))
    m = (xD - y_sgn) / (xD - x_sgn)
    Rmin = m / (1 - m)
ElseIf q = 0 Then
    y_sgn = zF
    x_sgn = y_sgn / (alfa + y_sgn * (1 - alfa))
    m = (xD - y_sgn) / (xD - x_sgn)
    Rmin = m / (1 - m)
End If
End Function

Function Nmin(xD As Double, xB As Double, alfa As Double) As Double
    Nmin = Log(xD * (1 - xB) / (1 - xD) / xB) / Log(alfa)
End Function

Function N_stadi_Eduljee(R As Double, Rmin As Double, Nmin As Double) As Double
Dim F As Double
Dim phi As Double

    F = (R - Rmin) / (R + 1)
    phi = 0.75 - 0.75 * (F) ^ (0.5668)
    N_stadi_Eduljee = (phi + Nmin) / (1 - phi)
End Function

Function N_stadi_Molokanov(R As Double, Rmin As Double, Nmin As Double) As Double
Dim F As Double
Dim phi As Double
    F = (R - Rmin) / (R + 1)
    phi = 1 - Exp(((1 + 54.4 * F) / (11 + 117.2 * F)) * ((F - 1) / (F) ^ (0.5)))
    N_stadi_Molokanov = (phi + Nmin) / (1 - phi)
End Function

Function xB_batchMultistadio(xD As Double, R As Double, N As Integer, alfa As Double) As Double
Dim x(1000) As Variant
Dim y(1000) As Variant
Dim i As Integer

    y(1) = xD
    For i = 1 To N
        x(i) = y(i) / (alfa - y(i) * (alfa - 1))
        y(i + 1) = R / (R + 1) * x(i) + xD / (R + 1)
    Next
    xB_batchMultistadio = x(N)
End Function

'ASSORBIMENTO
Function kremser_N_assorbimento(E As Double, XN1 As Double, Y0 As Double, YN As Double, k As Double) As Double
    Dim alfa As Double
    Dim Yeq As Double
        
    Yeq = k * XN1
    alfa = ((Yeq - Y0) - (YN - Y0)) / ((Yeq - Y0) - E * (YN - Y0))
    kremser_N_assorbimento = Log(alfa) / Log(E)
End Function

Function kremser_YN_assorbimento(E As Double, N As Double, XN1 As Double, Y0 As Double, k As Double) As Double
    Dim alfa As Double
    Dim beta As Double
    Dim Yeq As Double
    Yeq = k * XN1
    alfa = Y0 * (E) ^ (N) * (E - 1) / ((E) ^ (N + 1) - 1)
    beta = Yeq * ((E) ^ (N) - 1) / ((E) ^ (N + 1) - 1)
    kremser_YN_assorbimento = alfa + beta
End Function

Function kremser_N_nonLin_assorbimento(k As Double, YN As Double, Y0 As Double, XN1 As Double, LG As Double) As Double
    Dim a As Double
    Dim b As Double
    Dim D As Double
    Dim Yi As Double
    Dim delta As Double
    Dim num As Double
    Dim den As Double
    Dim teta1 As Double
    Dim teta2 As Double
    Dim teta3 As Double
    Dim phi As Integer
    Dim try As Double
    Dim min As Double
    Dim pi As Double
    pi = 3.14159265358979
    
    a = LG / (k - 1) + YN - LG * XN1
    b = k / (k - 1)
    D = b * (YN - LG * XN1)
    delta = (a - b) ^ 2 + 4 * D
    
    If delta >= 0 Then
        
        Yi = (a - b + (delta) ^ (0.5)) / 2
        
        num = 1 / (YN - Yi) - 1 / (a - b - 2 * Yi)
        den = 1 / (Y0 - Yi) - 1 / (a - b - 2 * Yi)
        kremser_N_nonLin_assorbimento = Log(num / den) / Log((a - Yi) / (b + Yi))
    Else
        teta1 = Atn((-delta) ^ (0.5) / (2 * YN - a + b))
        teta2 = Atn((-delta) ^ (0.5) / (2 * Y0 - a + b))
        teta3 = Atn((-delta) ^ (0.5) / (a + b))
        min = (teta2 - teta1) / teta3
        For phi = -100 To 100
            try = (teta2 - teta1 + phi * pi) / teta3
            If try < min And try > 0 Then
                min = (teta2 - teta1 + phi * pi) / teta3
            End If
        Next
        kremser_N_nonLin_assorbimento = min
    End If
End Function

Function LGmin_assorbimento(k As Double, XN1 As Double, YN As Double, Y0 As Double) As Double
Dim alfa As Double
Dim beta1 As Double
Dim beta2 As Double
Dim gamma1 As Double
Dim gamma2 As Double
Dim a As Double
Dim b As Double
Dim c As Double
Dim q1 As Double
Dim q2 As Double
Dim q3 As Double
Dim X0eq As Double
Dim X1 As Double
Dim x2 As Double
Dim y1 As Double
Dim y2 As Double

    X0eq = Y0 / (k + Y0 * (k - 1))
    alfa = 1 - k
    beta1 = 1 + k * XN1 - XN1
    beta2 = -k * YN + YN - k
    gamma1 = -XN1
    gamma2 = YN
    
    a = (beta1) ^ (2) - 4 * alfa * gamma1
    b = 2 * beta1 * beta2 - 4 * alfa * gamma2
    c = (beta2) ^ (2)
    If ((b) ^ (2) - 4 * a * c) >= 0 Then
        m1 = (-b + ((b) ^ (2) - 4 * a * c) ^ (0.5)) / (2 * a)
        m2 = (-b - ((b) ^ (2) - 4 * a * c) ^ (0.5)) / (2 * a)
        m3 = (Y0 - YN) / (X0eq - XN1)
        
        X1 = -(beta1 * m1 + beta2) / (2 * alfa * m1)
        x2 = -(beta1 * m2 + beta2) / (2 * alfa * m2)
        
        y1 = k * X1 / (1 + X1 * (k - 1))
        y2 = k * x2 / (1 + x2 * (k - 1))
        
        If y1 >= YN And y1 <= Y0 Then
            LGmin_assorbimento = m1
        ElseIf y2 >= YN And y2 <= Y0 Then
            LGmin_assorbimento = m2
        Else
            LGmin_assorbimento = m3
        End If
     Else
        LGmin_assorbimento = (Y0 - YN) / (X0eq - XN1)
     End If
End Function

'STRIPPING
Function kremser_N_stripping(a As Double, XN1 As Double, Y0 As Double, X1 As Double, k As Double) As Double
    Dim alfa As Double
    Dim Xeq As Double
        
    Xeq = Y0 / k
    alfa = ((Xeq - XN1) - (X1 - XN1)) / ((Xeq - XN1) - a * (X1 - XN1))
    kremser_N_stripping = Log(alfa) / Log(a)
End Function

Function kremser_X1_stripping(a As Double, N As Double, XN1 As Double, Y0 As Double, k As Double) As Double
    Dim alfa As Double
    Dim beta As Double
    Dim Xeq As Double
    Xeq = Y0 / k
    alfa = XN1 * (a) ^ (N) * (a - 1) / ((a) ^ (N + 1) - 1)
    beta = Xeq * ((a) ^ (N) - 1) / ((a) ^ (N + 1) - 1)
    kremser_X1_stripping = alfa + beta
End Function

Function kremser_N_nonLin_stripping(k As Double, X1 As Double, Y0 As Double, XN1 As Double, GL As Double) As Double
    Dim a As Double
    Dim b As Double
    Dim D As Double
    Dim Xi As Double
    Dim delta As Double
    Dim num As Double
    Dim den As Double
    Dim teta1 As Double
    Dim teta2 As Double
    Dim teta3 As Double
    Dim phi As Integer
    Dim try As Double
    Dim min As Double
    Dim pi As Double
    pi = 3.14159265358979
    
    a = 1 / (k - 1)
    b = GL * (Y0 + k / (k - 1)) - X1
    D = a * (GL * Y0 - X1)
    delta = (a - b) ^ 2 + 4 * D
    
    If delta >= 0 Then
        
        Xi = (a - b + (delta) ^ (0.5)) / 2
        
        num = 1 / (XN1 - Xi) - 1 / (a - b - 2 * Xi)
        den = 1 / (X1 - Xi) - 1 / (a - b - 2 * Xi)
        kremser_N_nonLin_stripping = Log(num / den) / Log((a - Xi) / (b + Xi))
    Else
        teta1 = Atn((-delta) ^ (0.5) / (2 * XN1 - a + b))
        teta2 = Atn((-delta) ^ (0.5) / (2 * X1 - a + b))
        teta3 = Atn((-delta) ^ (0.5) / (a + b))
        
        min = (teta2 - teta1) / teta3
        For phi = -100 To 100
            try = (teta2 - teta1 + phi * pi) / teta3
            If try < min And try > 0 Then
                min = (teta2 - teta1 + phi * pi) / teta3
            End If
        Next
        kremser_N_nonLin_stripping = min
    End If
End Function

Function GLmin_stripping(k As Double, XN1 As Double, X1 As Double, Y0 As Double) As Double
'DA SISTEMARE
Dim alfa As Double
Dim beta1 As Double
Dim beta2 As Double
Dim gamma1 As Double
Dim gamma2 As Double
Dim a As Double
Dim b As Double
Dim c As Double
Dim q1 As Double
Dim q2 As Double
Dim q3 As Double
Dim X0eq As Double
Dim X1_sgn As Double
Dim X2_sgn As Double

    Yeq = k * XN1 / (1 + XN1 * (1 - k))
    alfa = 1 - k
    beta1 = 1 + X1 * (k - 1)
    beta2 = Y0 * (1 - k) - k
    gamma1 = -X1
    gamma2 = Y0
    
    a = (beta1) ^ (2) - 4 * alfa * gamma1
    b = 2 * beta1 * beta2 - 4 * alfa * gamma2
    c = (beta2) ^ (2)
    If ((b) ^ (2) - 4 * a * c) >= 0 Then
        m1 = (-b + ((b) ^ (2) - 4 * a * c) ^ (0.5)) / (2 * a)
        m2 = (-b - ((b) ^ (2) - 4 * a * c) ^ (0.5)) / (2 * a)
        m3 = (Y0 - Yeq) / (X1 - XN1)
        
        X1_sgn = -(beta1 * m1 + beta2) / (2 * alfa * m1)
        X2_sgn = -(beta1 * m2 + beta2) / (2 * alfa * m2)
        
        If X1_sgn >= X1 And X1_sgn <= XN1 Then
            GLmin_stripping = m1
        ElseIf X2_sgn >= X1 And X2_sgn <= XN1 Then
            GLmin_stripping = m2
        Else
            GLmin_stripping = m3
        End If
     Else
        GLmin_stripping = (Y0 - Yeq) / (X1 - XN1)
     End If
End Function

Function Xi(wi As Variant, PM As Variant, NC As Integer, specie As Integer) As Double
Dim PMmix As Double
    PMmix = 0
    For i = 1 To NC
        If wi(i) <> 0 Then
            PMmix = PMmix + wi(i) / PM(i)
        End If
    Next
    PMmix = 1 / PMmix
    If wi(specie) <> 0 Then
        Xi = wi(specie) * PMmix / PM(specie)
    Else
        Xi = 0
    End If
End Function

Function wi(Xi As Variant, PM As Variant, NC As Integer, specie As Integer) As Double
Dim PMmix As Double
    PMmix = 0
    For i = 1 To NC
        PMmix = PMmix + Xi(i) * PM(i)
    Next
    wi = Xi(specie) * PM(specie) / PMmix
End Function

Function deltaT_LM(T_hot1 As Double, T_hot2 As Double, T_cold1 As Double, T_cold2 As Double) As Double
    deltaT_hot = T_hot1 - T_hot2
    deltaT_cold = T_cold1 - T_cold2
    deltaT_LM = (deltaT_hot - deltaT_cold) / Log(deltaT_hot / deltaT_cold)
End Function

Function NUT_1(S As Double, k As Double, y1 As Double, y2 As Double, x2 As Double) As Double
    NUT_1 = Log((y1 - k * x2) * (1 - S) / (y2 - k * x2) + S) / (1 - S)
End Function

Function NUT_2(GL As Double, k As Double, y1 As Double, y2 As Double, x2 As Double) As Double
Dim S As Double
    S = GL * k
    NUT_2 = Log((y1 - k * x2) * (1 - S) / (y2 - k * x2) + S) / (1 - S)
End Function

Function N_stadiSup_McCabeThiele(xD As Double, zF As Double, R As Double, alfa As Double) As Double
    Dim a As Double
    Dim b As Double
    Dim c As Double
    Dim delta As Double
    Dim logS1 As Double
    Dim logS2 As Double
    Dim logS As Double
    Dim logI As Double
    
    a = 1 / (alfa - 1)
    b = xD / R + alfa * (1 + R) / (R * (1 - alfa))
    c = xD / R / (alfa - 1)
    delta = 0.5 * (-(a + b) + ((a + b) ^ (2) - 4 * c) ^ (0.5))
    logS1 = 1 / (xD - delta) + 1 / (2 * delta + a + b)
    logS2 = 1 / (zF - delta) + 1 / (2 * delta + a + b)
    logS = Log(logS1 / logS2)
    logI = Log(-(a + delta) / (b + delta))
    N_stadiSup_McCabeThiele = logS / logI
End Function

Function N_stadiInf_McCabeThiele(xD As Double, xB As Double, zF As Double, zF_meno As Double, R As Double, alfa As Double, q As Double) As Double
    Dim a As Double
    Dim b As Double
    Dim c As Double
    Dim delta As Double
    Dim DF As Double
    Dim BF As Double
    Dim logS1 As Double
    Dim logS2 As Double
    Dim logS As Double
    Dim logI As Double
    BF = (xD - zF) / (xD - xB)
    DF = 1 - BF
    a = 1 / (alfa - 1)
    b = -((alfa - 1) * BF * xB + alfa * (R * DF + q - BF)) / ((alfa - 1) * (R * DF + q))
    c = -BF * xB / ((R * DF + q) * (alfa - 1))
    delta = 0.5 * (-(a + b) + ((a + b) ^ (2) - 4 * c) ^ (0.5))
    logS1 = 1 / (zF_meno - delta) + 1 / (2 * delta + a + b)
    logS2 = 1 / (xB - delta) + 1 / (2 * delta + a + b)
    logS = Log(logS1 / logS2)
    logI = Log(-(a + delta) / (b + delta))
    N_stadiInf_McCabeThiele = logS / logI
End Function

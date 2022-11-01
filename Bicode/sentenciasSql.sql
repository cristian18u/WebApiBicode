SELECT
    Nombre,
    Edad,
    CASE
        WHEN (S1.Edad <= 14) THEN 'Niño'
        WHEN (
            S1.Edad >= 15
            AND S1.Edad <= 20
        ) THEN 'Adolecente'
        WHEN (
            S1.Edad >= 21
            AND S1.Edad <= 60
        ) THEN 'Mayor de edad'
        ELSE 'Tercera edad'
    END AS Clasificacion
FROM
    (
        Select
            Nombre,
            (datediff(year, [FechaNacimiento], getdate())) AS Edad
        from
            Persona
    ) S1
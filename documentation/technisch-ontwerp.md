# Technisch Ontwerp

Voor het project moeten we een bepaalde route kunnen volgen, dit kan een aantal technische obstakels opleveren. één van deze obstakels is bijvoorbeeld de GPS. Voor de GPS moeten we coördinaten opvragen en willen hiervan de latitude en longitude terugkrijgen. Deze kunnen we krijgen door Unity’s ingebouwde systeem. Ook de richting van het kompas is met dit systeem op te vragen. Zo kan de speler weten waar het noorden is en kan hiermee dus makkelijker de route volgen. Ook willen we zorgen dat de speler zichzelf kan zien op een map. Zo weet de speler ook zonder kompas wat de route is en waar ze heen moeten gaan. Deze map gaan we halen vanuit de Google Maps API. Hiermee kunnen we de hoekpunten van de map krijgen (in longitude en latitude) zodat we positie van de speler relatief tot de map kunnen berekenen.

Hieronder word beter visueel uitgelegd hoe het kompas werkt

![technisch\_ontwerp](https://user-images.githubusercontent.com/40576028/220874231-b9740fa0-8881-4403-b56c-22d3e70df40b.png)

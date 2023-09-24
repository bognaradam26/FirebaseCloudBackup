# Jelenlegi állapot és tervek

## A rendszer jelenleg: 
Csak konzol állapotban fut, először ellenőrizzük a meglévő projekteket, ha vannak már projektek adva felsoroljuk őket és megkkérdezzük szeretne-e új projektetk hozzáadni, ha igen akkor elindul a az új projekt hozzáadásának menete, ha nem, akkor rákérdünk melyik projektnek szeretnénk biztosági mentést csinálni.

A backupService.BackupData() függvénye jelenleg json indetálását még javítani kell, de már visszaadja a collectionoket, jelenleg csak egy statikus projektet add vissza a bemutatás érdekében

## Tervek a jövőre nézve

1. A GUI segítségével a projektek felsorolása, hozzáadása vagy biztonsági mentése sokkal intuitívabb lesz
2. A kivételek megfelelő kezelése, nagyon sok "possible null value" warning-ot a már maga a grafikai felület is kezel, már csak azzal, hogy nem lehet a fieldet üresen hagyni
3. Úgy tervezem, hogy a Firestore adatbázisban szereplő root collectionok külön json fájlokba kerülnek így a collectionoket majd egyesével tudom lementeni, és tárolni a felhőben, így nem lesznek egyszerre localon 
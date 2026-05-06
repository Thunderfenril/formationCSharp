# 📘 Antisèche C# — Révisions Initiation

Document de révision couvrant tout ce qui a été vu dans les Séries I à IV (+ bonus Labyrinthe).

---

## Table des matières

1. [Bases du langage](#1-bases-du-langage)
2. [Tableaux et collections](#2-tableaux-et-collections)
3. [Algorithmes classiques](#3-algorithmes-classiques)
4. [Structures personnalisées](#4-structures-personnalisées)
5. [Strings](#5-strings)
6. [Fichiers](#6-fichiers)
7. [Gestion d'erreurs](#7-gestion-derreurs)
8. [Pièges fréquents](#8-pièges-fréquents)
9. [Réflexes pour l'exercice noté](#9-réflexes-pour-lexercice-noté)

---

## 1. Bases du langage

### 1.1 Types primitifs

| Type | Exemple de valeur | Quand l'utiliser |
|------|-------------------|------------------|
| `int` | `42`, `-7` | Entiers |
| `double` | `3.14` | Nombres à virgule |
| `bool` | `true`, `false` | Vrai/faux |
| `char` | `'A'` | Un seul caractère |
| `string` | `"Bonjour"` | Texte |

```csharp
int age = 25;
double note = 12.5;
bool estMajeur = age >= 18;
char initiale = 'A';
string nom = "Marc";
```

**Pourquoi c'est important** : choisir le bon type évite des conversions inutiles. Une note doit être `double` (pour les décimales), un compteur `int`, une présence ou non `bool`.

---

### 1.2 `switch` vs `if/else if`

`switch` compare une valeur à plusieurs **constantes**. Plus lisible que des `if` en chaîne.

```csharp
char ope = '+';
int a = 3, b = 4;
int resultat;

switch (ope)
{
    case '+': resultat = a + b; break;
    case '-': resultat = a - b; break;
    case '*': resultat = a * b; break;
    default: resultat = 0; break;
}
```

**Explication** : pour chaque valeur possible de `ope`, on définit le calcul. Le `break` empêche de tomber dans le cas suivant. Le `default` est obligatoire si on veut couvrir tous les cas non listés.

**Quand utiliser `if/else if`** : quand on compare avec des **plages** ou des conditions complexes :
```csharp
if (heure < 6) message = "Nuit";
else if (heure < 12) message = "Matin";
```

---

### 1.3 Les boucles

#### `for` — quand on connaît le nombre d'itérations

```csharp
for (int i = 0; i < tab.Length; i++)
{
    Console.WriteLine(tab[i]);
}
```

**Explication** : 3 parties séparées par `;` — initialisation `i = 0`, condition `i < tab.Length`, incrément `i++`. Idéal pour parcourir un tableau.

#### `while` — tant qu'une condition est vraie

```csharp
int saisie = -1;
while (saisie < 0 || saisie > 100)
{
    Console.Write("Entrez une note (0-100) : ");
    int.TryParse(Console.ReadLine(), out saisie);
}
```

**Explication** : on redemande tant que la saisie n'est pas dans `[0, 100]`. Utile quand on ne sait pas combien d'itérations.

#### `do/while` — au moins une exécution garantie

```csharp
string reponse;
do
{
    Console.Write("Continuer ? (o/n) : ");
    reponse = Console.ReadLine();
} while (reponse != "o" && reponse != "n");
```

**Explication** : on demande **avant** de tester. Le contraire du `while` classique.

---

### 1.4 `int.TryParse` — parser sans crasher

```csharp
string input = Console.ReadLine();
if (int.TryParse(input, out int valeur))
{
    Console.WriteLine($"OK : {valeur}");
}
else
{
    Console.WriteLine("Pas un entier valide.");
}
```

**Explication** : `TryParse` retourne `true` si le parsing a réussi (et stocke le résultat dans `valeur` via `out`), `false` sinon. **Jamais d'exception**, contrairement à `int.Parse` qui plante sur une saisie invalide.

---

### 1.5 Opérateur ternaire

```csharp
int a = 5, b = 8;
int max = (a > b) ? a : b;
```

**Explication** : équivalent compact de `if/else` qui retourne une valeur. À lire : « si `a > b` alors `a` sinon `b` ».

⚠️ **Piège de précédence** :
```csharp
return cond1 && cond2 ? false : true;
// est parsé comme :
return (cond1 && cond2) ? false : true;
// donc retourne false quand les conditions sont vraies — l'inverse de ce qu'on attend !
```

---

### 1.6 Récursivité vs itératif

```csharp
// Récursif
static int FactorialRec(int n)
{
    if (n <= 1) return 1;
    return n * FactorialRec(n - 1);
}

// Itératif
static int FactorialIter(int n)
{
    int res = 1;
    for (int i = 2; i <= n; i++) res *= i;
    return res;
}
```

**Explication** : la version récursive empile un nouvel appel à chaque tour (coût mémoire **O(n)**, risque de `StackOverflow`). La version itérative tient en mémoire **O(1)**. Pour les exercices simples, **préférer l'itératif**.

---

## 2. Tableaux et collections

### 2.1 Tableau 1D : `int[]`

```csharp
int[] notes = new int[5];
notes[0] = 12;
notes[1] = 15;

int[] preInit = { 12, 15, 8, 17, 10 };

Console.WriteLine(notes.Length);
```

**Explication** : taille **fixe** définie à la création. Indices de `0` à `Length - 1`.

---

### 2.2 Tableau 2D rectangulaire : `int[,]`

```csharp
int[,] grille = new int[3, 4];
grille[0, 0] = 1;
grille[2, 3] = 9;

int lignes = grille.GetLength(0);
int colonnes = grille.GetLength(1);
```

**Explication** : forme **rectangulaire** (toutes les lignes ont la même longueur). `GetLength(0)` = nombre de lignes, `GetLength(1)` = nombre de colonnes. Idéal pour une grille fixe (morpion 3×3, échiquier).

---

### 2.3 Tableau jagged : `int[][]`

```csharp
int[][] matrice = new int[3][];
for (int i = 0; i < 3; i++)
    matrice[i] = new int[4];
matrice[0][0] = 1;

int nbLignes = matrice.Length;
int nbColonnes = matrice[0].Length;
```

**Explication** : tableau **de tableaux**. Chaque ligne est elle-même un tableau qu'il faut **initialiser séparément**. Permet des lignes de tailles différentes.

⚠️ **Erreur classique** : oublier `matrice[i] = new int[m];` → `NullReferenceException` dès qu'on écrit dans `matrice[i][j]`.

⚠️ **`GetLength` ne marche pas** sur jagged ! Utiliser `.Length` et `[i].Length`.

---

### 2.4 `List<T>` — liste dynamique

```csharp
List<int> liste = new List<int>();
liste.Add(5);
liste.Add(8);
liste.Add(3);

Console.WriteLine(liste.Count);
Console.WriteLine(liste[0]);

liste.Remove(8);
liste.RemoveAt(0);
```

**Explication** : taille **dynamique**, on peut ajouter/retirer. `Count` est une **propriété** (pas `Count()` qui est LINQ et fait un appel inutile).

---

### 2.5 `Dictionary<K, V>` — recherche en O(1)

```csharp
Dictionary<string, int> notes = new Dictionary<string, int>();
notes["Marc"] = 15;
notes["Paul"] = 12;

if (notes.TryGetValue("Marc", out int note))
    Console.WriteLine($"Marc : {note}");

if (notes.ContainsKey("Anna"))
    Console.WriteLine("Anna existe");
```

**Explication** : association clé → valeur. `TryGetValue` est sécurisé (pas d'exception si la clé manque). Très utile pour les tables de correspondance (Morse, César, comptage).

---

### 2.6 `Stack<T>` — pile LIFO

```csharp
Stack<int> pile = new Stack<int>();
pile.Push(1);
pile.Push(2);
pile.Push(3);

int sommet = pile.Peek();
int retire = pile.Pop();
```

**Explication** : Last In First Out. `Push` ajoute, `Pop` retire et renvoie, `Peek` regarde sans retirer. Idéal pour un parcours en profondeur (DFS) sans récursion.

---

## 3. Algorithmes classiques

### 3.1 Recherche linéaire — O(n)

```csharp
static int LinearSearch(int[] tab, int valeur)
{
    if (tab == null || tab.Length == 0) return -1;
    for (int i = 0; i < tab.Length; i++)
        if (tab[i] == valeur) return i;
    return -1;
}
```

**Explication** : on parcourt de gauche à droite. Pire cas : la valeur n'est pas dans le tableau ou elle est en dernière position → **N éléments lus**.

---

### 3.2 Recherche dichotomique — O(log n)

```csharp
static int BinarySearch(int[] tab, int valeur)
{
    if (tab == null || tab.Length == 0) return -1;
    int gauche = 0, droite = tab.Length - 1;

    while (gauche <= droite)
    {
        int milieu = gauche + (droite - gauche) / 2;
        if (tab[milieu] == valeur) return milieu;
        if (tab[milieu] < valeur) gauche = milieu + 1;
        else droite = milieu - 1;
    }
    return -1;
}
```

**Explication** : nécessite un tableau **trié**. À chaque tour, on coupe l'espace de recherche en deux. Pire cas : **⌈log₂(N)⌉ + 1** comparaisons. Pour 1000 éléments, ~10 comparaisons au lieu de 1000.

⚠️ Pourquoi `gauche + (droite - gauche) / 2` plutôt que `(gauche + droite) / 2` ? Pour éviter un overflow d'`int` si les indices sont très grands.

---

### 3.3 Crible d'Ératosthène — O(n log log n)

```csharp
static int[] EratosthenesSieve(int n)
{
    if (n < 2) return new int[0];
    bool[] barre = new bool[n + 1];

    int limite = (int)Math.Sqrt(n);
    for (int i = 2; i <= limite; i++)
    {
        if (!barre[i])
            for (long k = (long)i * i; k <= n; k += i)
                barre[k] = true;
    }

    List<int> premiers = new List<int>();
    for (int i = 2; i <= n; i++)
        if (!barre[i]) premiers.Add(i);
    return premiers.ToArray();
}
```

**Explication** : on part avec tous les nombres `[2, n]` non barrés. Pour chaque `i` non barré jusqu'à √n, on barre tous ses multiples. À la fin, les non-barrés sont premiers.

⚠️ **Ne jamais utiliser `IsPrime`** dans le crible : tout l'intérêt est justement de ne pas tester la primalité.

---

### 3.4 Test de primalité — O(√n)

```csharp
static bool IsPrime(int n)
{
    if (n < 2) return false;
    if (n == 2) return true;
    if (n % 2 == 0) return false;

    int limite = (int)Math.Sqrt(n);
    for (int i = 3; i <= limite; i += 2)
        if (n % i == 0) return false;
    return true;
}
```

**Explication** : un diviseur `b` de `n` vérifie toujours `b ≤ √n` (sinon le quotient serait < √n et on l'aurait déjà trouvé). On teste 2 à part puis on saute de 2 en 2 pour ne tester que les impairs.

---

### 3.5 Algorithme d'Euclide — O(log min(a,b))

```csharp
static int Gcd(int a, int b)
{
    a = Math.Abs(a); b = Math.Abs(b);
    while (b != 0)
    {
        int r = a % b;
        a = b;
        b = r;
    }
    return a;
}
```

**Explication** : `pgcd(a, b) = pgcd(b, a % b)`. On répète jusqu'à `b = 0`, alors le PGCD est le `a` final. Très rapide même pour de grands nombres.

**Trace** pour `Gcd(48, 18)` :
- `a=48, b=18` → `r=12`, puis `a=18, b=12`
- `a=18, b=12` → `r=6`, puis `a=12, b=6`
- `a=12, b=6` → `r=0`, puis `a=6, b=0` → on sort, retour `6` ✓

---

### 3.6 Factorielle

```csharp
static int Factorial(int n)
{
    if (n < 0) throw new ArgumentException("n doit être positif.");
    int res = 1;
    for (int i = 2; i <= n; i++) res *= i;
    return res;
}
```

**Explication** : `n! = 1 × 2 × ... × n`. Limite : `int` déborde dès `13!`. Pour aller plus loin, utiliser `long` ou `BigInteger`.

---

## 4. Structures personnalisées

### 4.1 `struct` — type valeur

```csharp
public struct Point
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}

Point p1 = new Point(3, 4);
Point p2 = p1;
p2.X = 100;
Console.WriteLine(p1.X);
```

**Explication** : un `struct` est **copié par valeur**. Modifier `p2` ne change pas `p1`. Idéal pour des données légères et regroupées (coordonnées, paire de valeurs).

---

### 4.2 `class` — type référence

```csharp
public class Personne
{
    public string Nom;
    public int Age;
}

Personne p1 = new Personne { Nom = "Marc", Age = 25 };
Personne p2 = p1;
p2.Age = 99;
Console.WriteLine(p1.Age);
```

**Explication** : une `class` est **partagée par référence**. `p1` et `p2` pointent vers le **même** objet. Idéal quand on veut faire muter un objet via différentes parties du code.

---

### 4.3 `enum` — au lieu d'`int` magiques

```csharp
public enum CellStatus
{
    Normal,
    Start,
    End
}

Cell c = new Cell();
c.Status = CellStatus.Start;

if (c.Status == CellStatus.Start) Console.WriteLine("Entrée du labyrinthe");
```

**Explication** : un `enum` exprime un **ensemble fini** de valeurs nommées. Plus sûr qu'un `int` magique (impossible de mettre une valeur invalide), plus clair qu'un `bool` (un seul `bool` ne peut pas exprimer 3 états, et deux `bool` permettent l'incohérence des deux à `true`).

---

## 5. Strings

### 5.1 Méthodes de base

```csharp
string s = "  Bonjour Marc  ";

string nettoyé = s.Trim();
string maj = s.ToUpper();
string min = s.ToLower();
string remplacé = s.Replace("Marc", "Paul");

string sous = "Hello World".Substring(6, 5);

string[] parties = "a,b,c,d".Split(',');

int idx = "Hello World".IndexOf("World");
```

**Explication** : toutes ces méthodes **retournent une nouvelle chaîne** — les strings sont **immuables** en C#. Bien noter que `Substring(start, length)` prend un index de départ et une longueur.

---

### 5.2 `StringBuilder` — concaténation efficace

```csharp
StringBuilder sb = new StringBuilder();
for (int i = 0; i < 1000; i++)
{
    sb.Append("X");
}
string resultat = sb.ToString();
```

**Explication** : concaténer avec `+` dans une boucle est **catastrophique** (chaque `+` crée une nouvelle chaîne). `StringBuilder` est mutable et beaucoup plus rapide pour ce cas. Règle : si tu construis une chaîne **dans une boucle**, utilise `StringBuilder`.

---

### 5.3 Interpolation `$"..."`

```csharp
string nom = "Marc";
int age = 25;

string ancien = "Bonjour " + nom + ", vous avez " + age + " ans.";
string moderne = $"Bonjour {nom}, vous avez {age} ans.";
string formaté = $"Note : {12.345:0.0}";
```

**Explication** : l'interpolation `$"..."` insère directement la variable entre `{}`. Plus lisible et moins error-prone que la concaténation. On peut formater : `{x:0.00}` pour 2 décimales, `{x:000}` pour padder avec des zéros.

---

### 5.4 Tester un caractère

```csharp
char c = '5';

bool digit = char.IsDigit(c);
bool lettre = char.IsLetter(c);
bool espace = char.IsWhiteSpace(c);
bool maj = char.IsUpper(c);
```

**Explication** : ces méthodes statiques évitent d'écrire des conditions verbeuses comme `c >= '0' && c <= '9'`.

---

### 5.5 `string.Join` — concaténer un tableau

```csharp
int[] tab = { 1, 2, 3, 4 };
string s = string.Join(", ", tab);
```

**Explication** : insère le séparateur entre chaque élément, sans séparateur en début ou en fin. Beaucoup plus simple qu'une boucle avec un flag « est-ce le premier ? ».

---

## 6. Fichiers

### 6.1 Lecture

```csharp
if (!File.Exists("notes.csv"))
{
    Console.WriteLine("Fichier manquant.");
    return;
}

string[] lignes = File.ReadAllLines("notes.csv");
foreach (string ligne in lignes)
{
    Console.WriteLine(ligne);
}
```

**Explication** : `File.ReadAllLines` charge **tout le fichier** en mémoire d'un coup. Simple à utiliser pour de petits/moyens fichiers. Toujours vérifier `File.Exists` avant pour éviter une `FileNotFoundException`.

---

### 6.2 Écriture

```csharp
string[] lignes = { "Matière;Moyenne", "Histoire;10.0", "Maths;10.8" };
File.WriteAllLines("output.csv", lignes);
```

**Explication** : écrit ligne par ligne, écrase le fichier s'il existe. Pour ajouter à la fin : `File.AppendAllLines`.

---

### 6.3 Parser un nombre avec virgule décimale

```csharp
using System.Globalization;

string s = "12.5";
double d = double.Parse(s, CultureInfo.InvariantCulture);

string s2 = "12,5";
double d2 = double.Parse(s2.Replace(',', '.'), CultureInfo.InvariantCulture);
```

**Explication** : la culture **française** utilise `,` comme séparateur décimal, l'**anglaise** `.`. Sans `InvariantCulture`, `double.Parse("12.5")` plante en machine francophone. Toujours fixer la culture en parsing **et** en formatage pour des données techniques (CSV, JSON, etc.).

---

## 7. Gestion d'erreurs

### 7.1 Lever une exception

```csharp
static int Diviser(int a, int b)
{
    if (b == 0)
        throw new ArgumentException("Division par zéro impossible.", nameof(b));
    return a / b;
}

static int Racine(int n)
{
    if (n < 0)
        throw new ArgumentOutOfRangeException(nameof(n), n, "n doit être positif.");
    return (int)Math.Sqrt(n);
}
```

**Explication** : `ArgumentException` pour un argument invalide en général, `ArgumentOutOfRangeException` quand c'est une **borne** qui est dépassée. Le `nameof(b)` génère automatiquement la chaîne `"b"` — refactor-friendly.

---

### 7.2 Attraper une exception

```csharp
try
{
    int n = LireEntier("n : ");
    int res = Factorial(n);
    Console.WriteLine($"{n}! = {res}");
}
catch (ArgumentException e)
{
    Console.WriteLine($"Erreur : {e.Message}");
}
```

**Explication** : on **ne met un try/catch que là où une fonction peut réellement lever**. Sinon c'est du bruit qui masque les vrais problèmes.

---

### 7.3 Anti-pattern à éviter

```csharp
try
{
    bool ok = QcmValidity(qcm);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
```

**Explication** : si `QcmValidity` ne lève **rien** et retourne juste un `bool`, le `try/catch` est inutile et le résultat est ignoré. Bug classique.

**Bonne version** :

```csharp
if (!QcmValidity(qcm))
    throw new ArgumentException("QCM invalide.");
```

---

## 8. Pièges fréquents

### 8.1 Précédence du ternaire avec `&&`

```csharp
return cond1 && cond2 ? false : true;
```

**Problème** : équivalent à `(cond1 && cond2) ? false : true` → renvoie `false` quand les conditions sont vraies. Inverse logique.

**Solution** : parenthéser explicitement, ou écrire la logique correctement :

```csharp
return cond1 && cond2;
```

---

### 8.2 `Count()` (LINQ) vs `.Count` (propriété)

```csharp
List<int> l = new List<int> { 1, 2, 3 };

int n1 = l.Count();
int n2 = l.Count;
```

**Explication** : sur une `List<T>`, `Count` est une propriété en **O(1)**. `Count()` est une méthode LINQ qui ajoute un appel inutile. Préférer toujours la propriété.

---

### 8.3 Confusion `int[][]` vs `int[,]`

```csharp
int[][] jag = new int[3][];
int n = jag.Length;
int m = jag[0].Length;

int[,] rect = new int[3, 4];
int n2 = rect.GetLength(0);
int m2 = rect.GetLength(1);
```

**Explication** : `GetLength` n'existe **que** sur les rectangulaires (`int[,]`). Sur les jagged (`int[][]`), c'est `.Length` (lignes) et `[i].Length` (colonnes). Mélanger les deux est une erreur de compilation classique.

---

### 8.4 `RemoveAt` + `i++` saute un élément

```csharp
List<int> l = new List<int> { 1, 2, 3, 4 };
for (int i = 0; i < l.Count; i++)
{
    if (l[i] % 2 == 0)
        l.RemoveAt(i);
}
```

**Problème** : quand on retire l'index 1 (`2`), `3` prend sa place. Mais on incrémente `i` à 2 et on tombe sur `4`, en sautant `3`.

**Solution 1** : itérer en **arrière** :

```csharp
for (int i = l.Count - 1; i >= 0; i--)
{
    if (l[i] % 2 == 0) l.RemoveAt(i);
}
```

**Solution 2** : ne pas incrémenter quand on retire :

```csharp
int i = 0;
while (i < l.Count)
{
    if (l[i] % 2 == 0) l.RemoveAt(i);
    else i++;
}
```

---

### 8.5 Modulo négatif

```csharp
int x = -1 % 26;
```

**Explication** : en C#, le `%` peut renvoyer **négatif** si l'opérande gauche est négatif. Pour le code César par exemple, on veut un résultat dans `[0, 25]`.

**Solution** :

```csharp
int x = ((-1 % 26) + 26) % 26;
```

---

### 8.6 Hardcoder une taille

```csharp
for (int i = 0; i < 3; i++)
    for (int j = 0; j < 3; j++)
        matrice[i][j] = ...;
```

**Problème** : casse dès qu'on a une matrice non-3×3.

**Solution** :

```csharp
for (int i = 0; i < matrice.Length; i++)
    for (int j = 0; j < matrice[i].Length; j++)
        matrice[i][j] = ...;
```

---

### 8.7 Oublier d'initialiser une ligne d'un jagged

```csharp
int[][] m = new int[3][];
m[0][0] = 5;
```

**Problème** : `m[0]` vaut `null` → `NullReferenceException`.

**Solution** :

```csharp
int[][] m = new int[3][];
for (int i = 0; i < 3; i++) m[i] = new int[4];
m[0][0] = 5;
```

---

### 8.8 Copier-coller mal relu

```csharp
if (leftMatrix == null || leftMatrix == null) ...
```

**Problème** : `leftMatrix` est testé deux fois, `rightMatrix` n'est jamais testé.

**Solution** : toujours **relire les conditions composées** ligne par ligne.

---

## 9. Réflexes pour l'exercice noté

### 9.1 Avant de coder

1. **Lire l'énoncé en entier** avant de toucher au clavier.
2. Surligner les **signatures** : nom, types des paramètres, type de retour.
3. Repérer les **valeurs spéciales** à retourner : `-1` si vide, exception si invalide, message exact, etc.
4. Identifier les **structures de données** adaptées : `int[,]` pour une grille, `Dictionary` pour une correspondance, `Stack` pour un DFS.

### 9.2 Pendant le code

1. **Cas limites en premier** : `null`, vide, `n = 0`, `n négatif`, longueurs incompatibles.
2. **Saisies sécurisées** : `TryParse`, jamais `Parse` brut.
3. **Pas de hardcode** : toujours `.Length` ou `.Count`, jamais le nombre directement.
4. **Une fonction = une responsabilité** : si tu te retrouves à dupliquer 10 lignes, factorise.

### 9.3 Avant de rendre

1. **Tester avec l'exemple du sujet** : la sortie doit matcher caractère pour caractère.
2. **Tester les cas limites** : entrée vide, entrée invalide, valeur négative.
3. **Vérifier les messages exacts** : accents, espaces, ponctuation, casse.
4. **Compiler une dernière fois** sans warning.

### 9.4 Mini-quiz d'autodiagnostic

Si tu peux répondre/écrire ces points sans regarder, tu es prêt :

1. Écrire `Gcd(a, b)` avec une boucle `while`.
2. Écrire `BinarySearch` avec gauche/droite/milieu.
3. Écrire `IsPrime(n)` avec la borne `√n`.
4. Différence entre `int[,]` et `int[][]` : taille, accès, méthode pour la longueur.
5. Différence entre `struct` et `class` : copie par valeur vs par référence.
6. Quand utiliser `StringBuilder` plutôt que `+` ?
7. Pourquoi `int.TryParse` plutôt que `int.Parse` ?
8. Comment éviter le modulo négatif ?

Si tu bloques sur l'un de ces points, retourne sur la section correspondante.

---

**Bon courage pour demain !** 💪

---
theme: moon
transition: slide
logoimg: Vitas logo transparant.png
slideNumber: false
title: SOLID Workshop
---

<!-- .slide: data-background="Software transformation 2.png" -->
# SOLID Principles Workshop  


---

## Agenda  
<p class="fragment">1. <b>Introductie</b></p>
<p class="fragment">2. <b>Opzetten van de ontwikkelomgeving</b></p>
<p class="fragment">3. <b>Overzicht van de SOLID-principes</b></p>
<p class="fragment">4. <b>Eten</b></p>
<p class="fragment">5. <b>Praktische oefening: SOLID-overtredingen</b></p>
<p class="fragment">6. <b>Pull Requests & Peer Reviews</b></p>
<p class="fragment">7. <b>Afsluiting</b></p>

---
## Wie ben ik 👋  

- **Martijn van den Hoven** 

- .NET Developer sinds 2013
	- Bij Vitas sinds Nov 2023
- TDD, Automated Testing, Clean Code, Continuous Delivery
- Muziek, calisthenics, binnenkort vader

---

## Wie zijn jullie

1. **Naam en waar je huis woont** 🏠
2. **Huidige klus en rol** 💼
3. **Je ervaring met softwareontwikkeling**🔍  
4. **Wat vind je leuk om in je vrije tijd te doen?** 🎨

---
## Opzetten ontwikkelomgeving
- https://dev.azure.com/vitasit/Vitas%20Academy%20Students/_git/SOLID-Workshop
- Git clone
- Visual Studio, VSCode, Rider

---
## PR Workflow  
1. Nieuwe branch vanaf main
2. Changes committen
3. Pull request aanmaken
4. Werk van elkaar nakijken
5. We mergen er één naar main en gaan daarop verder

---

## What Are the SOLID Principles?  

_Robert C. Martin (Uncle Bob)_

<p class="fragment"><b>S</b>ingle Responsibility Principle (SRP)</p>  
<p class="fragment"><b>O</b>pen/Closed Principle (OCP)</p>  
<p class="fragment"><b>L</b>iskov Substitution Principle (LSP)</p>  
<p class="fragment"><b>I</b>nterface Segregation Principle (ISP)</p>  
<p class="fragment"><b>D</b>ependency Inversion Principle (DIP)</p>  
---

## Waarom?

<p class="fragment">- Reduceren van Complexiteit</p>
<p class="fragment">- Aanpassen wordt makkelijker (risico, grootte)</p>
<p class="fragment">- Voorspelbaarheid van het systeem</p>
<p class="fragment">- Beter testbaar</p>
<p class="fragment">- Meer flexibiliteit</p>

<p class="fragment"><b>Het omgekeerde hiervan zijn indicaties van slecht opgezette code</b></p>

---
## **S**ingle Responsibility Principle (SRP) 

> "A class should have only one reason to change."

<p class="fragment">✅ Een <b>unit</b> moet één taak hebben .</p>
<p class="fragment">✅ Als een eenheid om meerdere redenen verandert, <b>splits het op</b>.</p>


--



```csharp

public bool AuthenticateUser(string username, string password)
{
	// Step 1: Validate credentials
	var user = database.GetUserByUsername(username);
	if (user == null || user.Password != password)
	{
		// Step 2: Log failed attempt
		logger.Log($"Failed login attempt for user: {username}");

		// Step 3: Send security alert email
		emailService.Send(user.Email, "Security Alert", "There was a failed login attempt on your account.");
		return false;
	}

	return true;
}


```

```csharp
    public bool AuthenticateUser(string username, string password)
    {
        if (!_validator.IsValidUser(username, password))
        {
            _logger.LogFailedAttempt(username);
            _notifier.NotifyUserOfFailedLogin(username);
            return false;
        }

        return true;
    }
```

---

## **O**pen/Closed Principle (OCP)

> "Software entities should be open for extension, but closed for modification."
 
<p class="fragment">✅ Voeg nieuwe functionaliteit toe <b>zonder bestaande code te wijzigen</b>.</p>
<p class="fragment">✅ Gebruik <b>abstractie</b> (interfaces, inheritance of composition).</p>
<p class="fragment">✅ Minder risico op het stuk maken van bestaande code.</p>

--

```csharp
public decimal CalculateDiscount(decimal price, string discountType)
{
	if (discountType == "BlackFriday")
	{
		return price * 0.2m; // 20% discount
	}
	else if (discountType == "Loyalty")
	{
		return price * 0.1m; // 10% discount
	}
	
	return 0; // No discount
}
```

```csharp
public class BlackFridayDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal price) => price * 0.2m;
}

public class LoyaltyDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal price) => price * 0.1m;
}

public class DiscountService
{
    private readonly IDiscountStrategy _discountStrategy;
	
	// Juiste Strategy door Dependency Injection (Framework) of Factory Pattern
    public DiscountService(IDiscountStrategy discountStrategy)
    {
        _discountStrategy = discountStrategy;
    }

    public decimal CalculateDiscount(decimal price)
    {
        return _discountStrategy.ApplyDiscount(price);
    }
}
```

---

## **L**iskov Substitution Principle (LSP)

> "Derived classes must be substitutable for their base class."

<p class="fragment">✅ Gedrag <b>uitbreiden</b>, niet <b>veranderen</b>.</p>
<p class="fragment">✅ Vermijd overrides als dit bestaande <b>verwachtingen verbreekt</b>.</p>
<p class="fragment">✅ Heroverweeg inheritance (interfaces vs. abstract vs. geen inheritance).</p>


--

```csharp
public class Bird
{
    public virtual void Fly()
    {
        Console.WriteLine("The bird is flying.");
    }
}
```
```csharp
// Works fine
MakeBirdFly(new Sparrow());

// ❌ Breaks expectations! Throws exception
MakeBirdFly(new Penguin()); 
```
```csharp
public class Sparrow : Bird, IFlyable // <--
{
    public void Fly()
    {
        Console.WriteLine("The sparrow is flying.");
    }
}

public class Penguin : Bird
{
    public void Swim()
    {
        Console.WriteLine("The penguin is swimming.");
    }
}
```

---

## **I**nterface Segregation Principle (ISP)

> "Clients should not be forced to depend on interfaces they do not use."


<p class="fragment">✅ Houd interfaces <b>klein en gefocust</b>. (SRP)</p>
<p class="fragment">✅ Vermijd <b>grote interfaces</b> die klassen dwingen om irrelevante methoden te implementeren.</p>

--

```csharp
public interface IPrinter
{
    void Print(string content);
    void Scan(string content);
}
```

```csharp
public class BasicPrinter : IPrinter
{
    public void Print(string content)
    {
        Console.WriteLine("Printing: " + content);
    }

    public void Scan(string content)
    {
        throw new NotImplementedException("Scan not supported!");
    }
}
```
```csharp
public interface IPrint
{
    void Print(string content);
}

public interface IScan
{
    void Scan(string content);
}
```

---

## **D**ependency Inversion Principle (DIP)

> "Depend on abstractions, not concrete implementations."

<p class="fragment">✅ Programmeer naar <b>interfaces</b>, niet concrete klassen.</p>
<p class="fragment">✅ Gebruik <b>Dependency Injection</b></p>

--
```csharp
public OrderProcessor()
{
	// 1. Niet te unit testen
	// 2. Ingrijpend als het moet veranderen
	_sqlDatabase = new SqlDatabase();
}
```

```csharp
public OrderProcessor(IOrderRepository orderRepository)
{
	_orderRepository = orderRepository;
}
```

---

## Praktische tips

<p class="fragment">- Fit for Purpose/overengineering</p>
<p class="fragment">- Pragmatisch/dogmatisch</p>
<p class="fragment">- Scope creep, kleine stappen</p>
<p class="fragment">- Unit tests</p>
<p class="fragment">- Pick your battles!</p>

---
## Reflection & Feedback  

- Wat vond je lastig?
- Wat kan je meenemen naar je klus?

---

## Wil je meer weten?

<p class="fragment"><b>Vervolg workshop Code Smells<b></p>
<p class="fragment">📖<h3>Boeken</h3></p>
<p class="fragment">Refactoring by Martin Fowler</p>
<p class="fragment">Clean Code Robert C. Martin (korreltje zout)</p>
<p class="fragment">The Pragmatic Programmer by David Thomas and Andrew Hunt 🎧</p>
<p class="fragment">📺<h3>Youtube</h3></p>
<p class="fragment">Uncle Bob</p>
<p class="fragment">Zoran Horvat</p>

---

<!-- .slide: data-background="#1abc9c" -->

# Bedankt!
### Maak iets waar je trots op bent!

**Questions? Feedback?**  
- 📧 [mvandenhoven@vitas.nl](mailto:mvandenhoven@vitas.nl)  
- 💬**MSTeams** Martijn van den Hoven


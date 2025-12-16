# 🥕 VR Flea Market Experience (Meta Interaction SDK)

This project is a **VR flea market environment** built in **Unity 2022.3 LTS** using the **Meta Interaction SDK (Oculus Interaction)**. The experience is designed as a large-scale narrative space containing multiple smaller interactive exhibits (produce stalls, baskets, NPCs, ambient animals), similar to museum or themed exhibition design.

Visitors can freely explore the market, grab fruits and vegetables, view price UI, hear ambient sounds, and interact with NPCs using **hand tracking or controllers**.

---

## 🎯 Core Design Concept

* **Large-scale container:** Flea market environment acts as a unifying narrative space
* **Smaller interactive exhibits:** Individual fruits, baskets, NPCs, sound-reactive animals
* **Free exploration:** Non-linear navigation between stalls
* **Multi-sensory interaction:** Grab, touch, UI popups, sound, animation

---

## 🛠 Tech Stack

* **Unity:** 2022.3.62f3 (LTS)
* **VR SDK:** Meta Interaction SDK (Oculus Interaction)
* **Target Device:** Meta Quest (2 / 3)
* **Input:** Hand Tracking + Controllers

---

## 📁 Project Structure (Key Assets)

```
Assets/
├── Naman's Assets/
│   ├── Scripts/
│   │   ├── ShowUIOnGrab_Meta.cs
│   │   ├── EnablePhysicsOnGrab_Meta.cs
│   │   ├── CowMooTimer.cs
│   │   └── ChefWelcome.cs
│   ├── Prefabs/
│   │   ├── Fruits (Apple, Watermelon, Banana)
│   │   ├── Basket
│   │   └── Chef NPC
│   └── UI/
│       └── PriceUI (World Space Canvas)
```

---

## ✋ Interaction System (IMPORTANT)

This project uses **Meta Interaction SDK**, NOT XR Interaction Toolkit.

### ❌ What does NOT work

* XR Grab Interactable events
* OnTriggerEnter for hands
* Unity Event Triggers

### ✅ What DOES work

* `Grabbable`
* Meta `PointerEvent` system
* `OneGrabFreeTransformer`

All interactions are driven using:

```csharp
using Oculus.Interaction;
```

---

## 🍎 Fruit Interaction Logic

### Behavior

* Fruits stay inside boxes at scene start
* Fruits do **not** fall due to gravity
* Gravity enables **only when grabbed**
* UI shows price **only when grabbed**

### Why this is needed

Physics starts immediately in VR and causes jitter/falling if gravity is enabled at load time. This is standard practice in museum-quality VR experiences.

---

## 📜 Fruit Scripts

### 1️⃣ Enable Physics Only When Grabbed

```csharp
using UnityEngine;
using Oculus.Interaction;

public class EnablePhysicsOnGrab_Meta : MonoBehaviour
{
    private Rigidbody rb;
    private Grabbable grabbable;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabbable = GetComponent<Grabbable>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    void OnEnable()
    {
        grabbable.WhenPointerEventRaised += HandleEvent;
    }

    void OnDisable()
    {
        grabbable.WhenPointerEventRaised -= HandleEvent;
    }

    void HandleEvent(PointerEvent evt)
    {
        if (evt.Type == PointerEventType.Select)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }
}
```

---

### 2️⃣ Show Price UI on Grab

```csharp
using UnityEngine;
using Oculus.Interaction;

public class ShowUIOnGrab_Meta : MonoBehaviour
{
    public GameObject priceUI;
    private Grabbable grabbable;

    void Awake()
    {
        grabbable = GetComponent<Grabbable>();
        priceUI.SetActive(false);
    }

    void OnEnable()
    {
        grabbable.WhenPointerEventRaised += HandlePointerEvent;
    }

    void OnDisable()
    {
        grabbable.WhenPointerEventRaised -= HandlePointerEvent;
    }

    void HandlePointerEvent(PointerEvent evt)
    {
        if (evt.Type == PointerEventType.Select)
            priceUI.SetActive(true);
        else if (evt.Type == PointerEventType.Unselect)
            priceUI.SetActive(false);
    }
}
```

---

## 🧺 Basket System (Concept)

* Basket uses **trigger collider**
* Fruits remain grabbable
* Future expansion: count items + checkout UI

---

## 🐄 Cow Ambient Interaction

* Cow plays **moo sound every 10 seconds**
* Sound is independent of grabbing items
* Adds environmental storytelling and humor

---

## 👩‍🍳 Chef NPC Interaction

### Behavior

* Chef stands at stall
* Plays animation when player comes near
* Says: **"Hi! Welcome!"**

### Trigger Method

* Proximity-based (Collider + IsTrigger)
* Animation controlled via Animator
* AudioSource attached to NPC

---

## 🧱 Collider Rules Used

### Fruits

* Rigidbody: Kinematic (start)
* Collider: Sphere / Box
* Gravity: Enabled on grab only

### Boxes & Stalls

* Box Collider
* Is Trigger: ❌ OFF
* Mesh Collider: ❌ Avoid

### Triggers (NPC / UI)

* Is Trigger: ✅ ON
* No Rigidbody required

---

## 🚨 Common Issues & Fixes

| Issue                 | Cause                  | Fix                              |
| --------------------- | ---------------------- | -------------------------------- |
| Fruits falling        | Gravity on at start    | Make Rigidbody kinematic         |
| UI not showing        | Using XR events        | Use Meta PointerEvents           |
| Can't assign Animator | Script on wrong object | Animator + script on same object |
| Hand grab not working | Missing Grabbable      | Add Grabbable + Transformer      |

---

## 🚀 Future Enhancements

* Checkout counter
* Basket total UI
* NPC dialogue trees
* Dynamic pricing
* Time-of-day lighting
* Multiplayer market

---

## ✅ Summary

This project demonstrates:

* Spatial storytelling
* Meta Interaction SDK best practices
* Stable VR physics handling
* Modular interactive exhibit design

Designed to mirror **real-world interactive museum & themed exhibition workflows** inside VR.

---

👋 Built as part of an interactive / spatial experience design project.

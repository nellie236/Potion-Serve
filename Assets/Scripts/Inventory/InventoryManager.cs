using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject itemCursor;

    [SerializeField] private GameObject slotHolder;
    [SerializeField] private GameObject hotbarSlotHolder;
    [SerializeField] private ItemClass itemToAdd;
    [SerializeField] private ItemClass itemToRemove;

    [SerializeField] private SlotClass[] startingItems;

    private SlotClass[] items;

    private GameObject[] slots;
    private GameObject[] hotbarSlots;

    private SlotClass movingSlot;
    private SlotClass tempSlot;
    private SlotClass originalSlot;

    bool isMovingItem;

    [SerializeField] private GameObject hotbarSelector;
    [SerializeField] private int selectedSlotIndex = 0;
    public ItemClass selectedItem;
    public SpriteRenderer displaySelectedItem;
    

    public Image InventoryPanel;
    public bool inventoryOn;
    public bool facingLeft;

    public bool inventoryFull;

    private void Start()
    {
        displaySelectedItem = GameObject.Find("selectedItem").GetComponent<SpriteRenderer>();
        slots = new GameObject[slotHolder.transform.childCount];
        items = new SlotClass[slots.Length];

        hotbarSlots = new GameObject[hotbarSlotHolder.transform.childCount];
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            hotbarSlots[i] = hotbarSlotHolder.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new SlotClass();
        }

        for (int i = 0; i < startingItems.Length; i++)
        {
            items[i] = startingItems[i];
        }
        //set all the slots
        for (int i = 0; i < slotHolder.transform.childCount; i++)
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
       
        RefreshUI();

        //Remove(itemToRemove);
        //Add(itemToAdd, 1);

        InventoryPanel.gameObject.SetActive(false);
        inventoryOn = false; 
        
    }

    private void Update()
    {
        itemCursor.SetActive(isMovingItem);
        itemCursor.transform.position = Input.mousePosition;
        if (isMovingItem)
            itemCursor.GetComponent<Image>().sprite = movingSlot.GetItem().itemIcon;

        if (Input.GetMouseButtonDown(0)) //we left clicked! 
        {
            //find closest slot we clicked on
            if (isMovingItem)
            {
                EndItemMove();
            }
            else
                BeginItemMove();
        }
        else if (Input.GetMouseButtonDown(1)) // we right clicked!
        {
            //find closest slot we clicked on
            if (isMovingItem)
            {
                EndItemMove_Single();
            }
            else
                BeginItemMove_Half();
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0) //scrolling up
        {
            selectedSlotIndex = Mathf.Clamp(selectedSlotIndex + 1, 0, hotbarSlots.Length - 1);
        }    
        else if (Input.GetAxis("Mouse ScrollWheel") < 0) //scrolling down
        {
            selectedSlotIndex = Mathf.Clamp(selectedSlotIndex - 1, 0, hotbarSlots.Length - 1);
        }

        hotbarSelector.transform.position = hotbarSlots[selectedSlotIndex].transform.position;
        selectedItem = items[selectedSlotIndex + (hotbarSlots.Length)].GetItem();

        //throw item
        if ((Input.GetKeyDown(KeyCode.Q)) && (selectedItem != null))
        {
            if (selectedItem.throwablePrefab == null)
            {
                return;
            }
            else if (selectedItem.throwablePrefab != null) 
            {
                //thrownItem.GetComponent<Projectile>().thrown = false;
                ThrowItem();
            }
            //experimenting here with throwing item
            
        }

        
        if (selectedItem != null)
        {
            displaySelectedItem.sprite = selectedItem.GetItem().itemIcon;
        }
        else if (selectedItem == null)
        {
            displaySelectedItem.sprite = null;
        }

    }

    public void ThrowItem()
    {
        // SlotClass selectedSlot = ContainsInHotbar(selectedItem);
        SlotClass selectedSlot = items[selectedSlotIndex + (hotbarSlots.Length)];

        GameObject thrownItem = Instantiate(selectedItem.throwablePrefab, GameObject.Find("Player").transform.GetChild(0).transform) as GameObject;
        thrownItem.transform.parent = null;

        //thrownItem.GetComponent<Projectile>().thrown = true;

        if (GameObject.Find("Player").GetComponent<CharacterController2D>().facingRight == true)
        {
            //facing right
            facingLeft = false;

        }
        else if (GameObject.Find("Player").GetComponent<CharacterController2D>().facingRight == false)
        {
            //facing left
            facingLeft = true;
        }

        //thrownItem.GetComponent<Projectile>().target.transform.position = thrownItem.transform.position;

        if (selectedSlot.GetQuantity() >= 1)
        {
            selectedSlot.SubQuantity(1);
        }

        if (selectedSlot.GetQuantity() < 1)
        {
            selectedSlot.Clear();
        }

        RefreshUI();
    }

    public void GiveCustomerDesired(ItemClass desired, GameObject currentCustomer)
    {
        if (selectedItem != null)
        {
            //SlotClass selectedSlot = ContainsInHotbar(selectedItem);
            SlotClass selectedSlot = items[selectedSlotIndex + (hotbarSlots.Length )];
            if (selectedItem == desired)
            {
                currentCustomer.GetComponent<CustomerAgent>().orderFulfilled = true;
                //give to customer, change dialogue to success and give money
                selectedSlot.Clear();
            }
        }
        RefreshUI();
    }

    public void SwitchInventory()
    {
        switch (inventoryOn)
        {
            case true:
                InventoryPanel.gameObject.SetActive(false);
                inventoryOn = false;
                if (isMovingItem)
                {
                    EndItemMove();
                }
                break;
            case false:
                InventoryPanel.gameObject.SetActive(true);
                inventoryOn = true;
                break;
        }
    }

    #region Inventory Utils
    public void RefreshUI()
    {
        int filledSlots = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].GetItem().itemIcon;
                if (items[i].GetItem().isStackable)
                    slots[i].transform.GetChild(1).GetComponent<Text>().text = items[i].GetQuantity() + "";
                else
                    slots[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                slots[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }

            int slotTotal = slots.Length;
            
            if (slots[i].transform.GetChild(0).GetComponent<Image>().sprite != null)
            {
                filledSlots++; 
            }

            if (filledSlots == slotTotal)
            {
                inventoryFull = true;
            }
            else if (filledSlots != slotTotal)
            {
                inventoryFull = false;
            }
        }

        //Debug.Log(filledSlots);


        RefreshHotbar();
    }

    public void RefreshHotbar()
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            try
            {
                hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i + (hotbarSlots.Length )].GetItem().itemIcon;
                if (items[i + (hotbarSlots.Length )].GetItem().isStackable)
                    hotbarSlots[i].transform.GetChild(1).GetComponent<Text>().text = items[i + (hotbarSlots.Length )].GetQuantity() + "";
                else
                    hotbarSlots[i].transform.GetChild(1).GetComponent<Text>().text = "";
                
            }
            catch
            {
                hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                hotbarSlots[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }
        }
    }
    
    public bool Add(ItemClass item, int quantity)
    {
        //items.Add(item);

        // check if inventory contains the same item
        SlotClass slot = Contains(item);
        if (slot != null && slot.GetItem().isStackable)
            slot.AddQuantity(quantity);
        else
        {
            /*for (int i = 0; i < items.Length; i++)
            {
                if (items[i].GetItem() == null) // this is an empty slot
                {
                    items[i].AddItem(item, quantity);
                    break;
                }
            }*/

            for (int i = items.Length - 1; i > -1; i--)
            {
                //Debug.Log("Item is +" + i);
                if (items[i].GetItem() == null)
                {
                    items[i].AddItem(item, quantity);
                    break;
                }
            }
        }

        RefreshUI();
        return true;
    }

    
    public bool Remove(ItemClass item)
    {
        //items.Remove(item);
        SlotClass temp = Contains(item);
        if (temp != null)
        {
            if (temp.GetQuantity() >= 1)
            {
                temp.SubQuantity(1);
            }
            else
            {
                int slotToRemoveIndex = 0;
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].GetItem() == item)
                    {
                        slotToRemoveIndex = i;
                        break;
                    }
                }

                items[slotToRemoveIndex].Clear();
            }
        }
        else
        {
            return false;
        }
            
        RefreshUI();
        return true;
    }
    
    public SlotClass Contains(ItemClass item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].GetItem() == item)
                return items[i];
        }
        return null;
    }

    public SlotClass ContainsInHotbar(ItemClass item)
    {
        for (int h = items.Length - 1; h >= 14; h--)
        {
            if (items[h].GetItem() == selectedItem)
                return items[h];
        }
        return null;
    }

    #endregion Inventory Utils


    #region Moving Items
    private bool BeginItemMove()
    {
        originalSlot = GetClosestSlot();
        if (originalSlot == null || originalSlot.GetItem() == null)
            return false; //there isnt an item to move

        movingSlot = new SlotClass(originalSlot); //error
        originalSlot.Clear();
        isMovingItem = true;
        RefreshUI();
        return true;
    }

    private bool BeginItemMove_Half()
    {
        originalSlot = GetClosestSlot();
        if (originalSlot == null || originalSlot.GetItem() == null)
            return false; //there isnt an item to move

        movingSlot = new SlotClass(originalSlot.GetItem(), Mathf.CeilToInt(originalSlot.GetQuantity() / 2f)); 
        originalSlot.SubQuantity(Mathf.CeilToInt(originalSlot.GetQuantity() / 2f));
        if (originalSlot.GetQuantity() == 0)
            originalSlot.Clear();

        isMovingItem = true;
        RefreshUI();
        return true;
    }

    private bool EndItemMove()
    {
        originalSlot = GetClosestSlot();
        if (originalSlot == null)
        {
            Add(movingSlot.GetItem(), movingSlot.GetQuantity());
            movingSlot.Clear();
        }
        else
        {
            if (originalSlot.GetItem() != null)
            {
                if (originalSlot.GetItem() == movingSlot.GetItem()) //they're the same item
                {
                    if (originalSlot.GetItem().isStackable)
                    {
                        originalSlot.AddQuantity(movingSlot.GetQuantity());
                        movingSlot.Clear();
                    }
                    else
                        return false;
                }
                else //not the same item / not stackable - swap items
                {
                    tempSlot = new SlotClass(originalSlot); //a = b 
                    originalSlot.AddItem(movingSlot.GetItem(), movingSlot.GetQuantity()); // b = c
                    movingSlot.AddItem(tempSlot.GetItem(), tempSlot.GetQuantity()); // a = c
                    RefreshUI();
                    return true;
                }
            }
            else //place item as usual
            {
                originalSlot.AddItem(movingSlot.GetItem(), movingSlot.GetQuantity());
                movingSlot.Clear();
            }
        }

        isMovingItem = false;
        RefreshUI();
        return true;
    }

    private bool EndItemMove_Single()
    {
        originalSlot = GetClosestSlot();
        if (originalSlot == null)
            return false; //there isnt an item to move
        else if (originalSlot.GetItem() != null && originalSlot.GetItem() != movingSlot.GetItem())
        {
            return false; 
        }

        movingSlot.SubQuantity(1);
        if (originalSlot.GetItem() != null && originalSlot.GetItem() == movingSlot.GetItem())
            originalSlot.AddQuantity(1);
        else
            originalSlot.AddItem(movingSlot.GetItem(), 1);

        if (movingSlot.GetQuantity() < 1)
        {
            isMovingItem = false;
            movingSlot.Clear();
        }
        else
            isMovingItem = true;

        RefreshUI();
        return true;
    }

    private SlotClass GetClosestSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (Vector2.Distance(slots[i].transform.position, Input.mousePosition) <= 32)
                return items[i];
        }
        return null;
    }
    #endregion Moving Items
}

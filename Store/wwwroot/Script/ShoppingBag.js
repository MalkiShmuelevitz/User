let cart = JSON.parse(sessionStorage.getItem("cart")) || []
let totalPrice = 0
const load = addEventListener("load", () => {
    drawProductInCart()
})
const totalAmountAndPrice = (total) => {
    document.getElementById("itemCount").innerHTML = cart.length
    document.getElementById("totalAmount").innerHTML = `$${total}`

}
const drawProductInCart = () => {
    cart.forEach((cart) => {
        totalPrice += cart.price;
        drawOneProductInCart(cart);
    });
    totalAmountAndPrice(totalPrice)
}
const drawOneProductInCart = (productInCart) => {
    let url = `Image/${productInCart.picture}`
    let tmp = document.getElementById("temp-row")
    let cloneProductInCart = tmp.content.cloneNode(true)
    cloneProductInCart.querySelector(".image").style.backgroundImage = `url(${url})`
    cloneProductInCart.querySelector(".itemName").innerText = productInCart.productName
    cloneProductInCart.querySelector(".price").innerText = `$${productInCart.price}`
    cloneProductInCart.querySelector(".expandoHeight").addEventListener("click", () => { deleteProductInCart(productInCart.id) })
    document.querySelector("tbody").appendChild(cloneProductInCart)
}
const deleteProductInCart = (pId) => {
    let pid = cart.findIndex((c) => c.id == pId)
    cart.splice(pid, 1)
    sessionStorage.setItem("cart", JSON.stringify(cart))
    document.querySelector("tbody").innerHTML = ""
    totalPrice = 0
    drawProductInCart()
}
const placeOrder = async () => {
    if (!JSON.parse(sessionStorage.getItem("id"))) {
        alert("User not found, Go to login")
        window.location.href = 'login.html'
    }
    else {
        const order = createOrder()
        if (order.orderItems.length == 0) {
            alert("You hav'nt products in your order")
        }
        else {
            console.log(order)
            try {
                const data = await fetch("api/Orders", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(order)
                })
                if (data.status == 400) {
                    throw new Error(`Your order not complete ${data.title}`)
                }
                else if (data.status == 204) {
                    throw new Error(`You hav'nt products in your order`)
                }
                //if (orderData.orderItems.length)
                //    alert(`your cart is empty 😫`)
                else if (data.status == 401) {
                    throw new Error(`YOU CAN NOT Complete your order 😪`)
                }
                else {
                    let orderData = await data.json()
                    alert(`order ${orderData.id} was placed successfully!!!`)
                    sessionStorage.setItem("cart", JSON.stringify([]))
                    window.location.href = "products.html"
                }
                //else {

                //}
            }
            catch (error) {
                alert(error)
                //console.log(error)
            }
        }
        
    }
    
}
const createOrder = () => {
    const newOrderItems = cart.map((c) => {
        return {
            "productId": c.id,
            "quantity": 1
        }
    })
    let order = {
        "orderDate": new Date(),
        "orderSum": totalPrice,
        "userId": JSON.parse(sessionStorage.getItem("id")) || "",
        "orderItems": newOrderItems
    }
    return order;
}


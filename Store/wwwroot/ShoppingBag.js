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
    
    for (let i = 0; i < cart.length; i++) {
        totalPrice += cart[i].price
        drawOneProductInCart(cart[i])
    }
    totalAmountAndPrice(totalPrice)
}
const drawOneProductInCart = (productInCart) => {
    let url = `Image/${productInCart.picture}`
    let tmp = document.getElementById("temp-row")
    let cloneProductInCart = tmp.content.cloneNode(true)
    cloneProductInCart.querySelector(".image").style.backgroundImage = `url(${url})`
    cloneProductInCart.querySelector(".itemName").innerText = productInCart.productName
    cloneProductInCart.querySelector(".price").innerText = `$${productInCart.price}`
    cloneProductInCart.querySelector(".expandoHeight").addEventListener("click", () => { deleteProductInCart(productInCart.productName) })
    document.querySelector("tbody").appendChild(cloneProductInCart)
}
const deleteProductInCart = (pName) => {
    let pid = cart.findIndex((c) => c.productName == pName)
    console.log(pid)
    cart.splice(pid, 1)
    console.log(cart)
    sessionStorage.setItem("cart", JSON.stringify(cart))
    document.querySelector("tbody").innerHTML = ""
    drawProductInCart()
}
const placeOrder = async () => {
    const order = createOrder()
    console.log(order)
    try {
        const data = await fetch("api/Orders", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(order)
        })
        let orderData = await data.json()
        if (data.status == 201) {
            console.log(orderData)
            alert(`Your order received seccessfully!!!`)
            sessionStorage.setItem("cart", JSON.stringify([]))
            //window.location.reload()
        }
        if (data.status == 400) {
            alert(`Your order not complete ${data.title}`)
        }
        //else {

        //}
    }
    catch (error) {
        alert(`Your order not complete because: ${error}`)
        console.log(error)
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


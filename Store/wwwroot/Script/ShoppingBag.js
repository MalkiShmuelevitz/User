let cart = JSON.parse(sessionStorage.getItem("cart")) || [];
let totalPrice = 0;

window.addEventListener("load", () => {
    drawProductInCart();
});

const totalAmountAndPrice = (total) => {
    document.getElementById("itemCount").textContent = cart.length;
    document.getElementById("totalAmount").textContent = `$${total}`;
};

const drawProductInCart = () => {
    cart.forEach((cart) => {
        totalPrice += cart.price;
        drawOneProductInCart(cart);
    });
    totalAmountAndPrice(totalPrice);
};

const drawOneProductInCart = (productInCart) => {
    const url = `Image/${productInCart.picture}`;
    const template = document.getElementById("temp-row");
    const cloneProductInCart = template.content.cloneNode(true);

    cloneProductInCart.querySelector(".image").style.backgroundImage = `url(${url})`;
    cloneProductInCart.querySelector(".itemName").textContent = productInCart.productName;
    cloneProductInCart.querySelector(".price").textContent = `$${productInCart.price}`;
    cloneProductInCart.querySelector(".expandoHeight").addEventListener("click", () => {
        deleteProductInCart(productInCart.id);
    });

    document.querySelector("tbody").appendChild(cloneProductInCart);
};

const deleteProductInCart = (pId) => {
    const index = cart.findIndex((c) => c.id === pId);
    cart.splice(index, 1);
    sessionStorage.setItem("cart", JSON.stringify(cart));
    document.querySelector("tbody").innerHTML = "";
    totalPrice = 0;
    drawProductInCart();
};

const placeOrder = async () => {
    if (!JSON.parse(sessionStorage.getItem("id"))) {
        alert("User not found, Go to login");
        window.location.href = 'login.html';
    } else {
        const order = createOrder();
        if (order.orderItems.length === 0) {
            alert("You haven't products in your order");
        } else {
            try {
                const response = await fetch("api/Orders", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(order)
                });

                if (response.status === 400) {
                    throw new Error(`Your order not complete ${response.title}`);
                } else if (response.status === 204) {
                    throw new Error(`You haven't products in your order`);
                } else if (response.status === 401) {
                    throw new Error(`YOU CAN NOT Complete your order`);
                } else {
                    const orderData = await response.json();
                    alert(`Order ${orderData.id} was placed successfully!`);
                    sessionStorage.setItem("cart", JSON.stringify([]));
                    window.location.href = "products.html";
                }
            } catch (error) {
                alert(error);
                console.error(error);
            }
        }
    }
};

const createOrder = () => {
    const newOrderItems = cart.map((c) => ({
        "productId": c.id,
        "quantity": 1
    }));

    const order = {
        "orderDate": new Date(),
        "orderSum": totalPrice,
        "userId": JSON.parse(sessionStorage.getItem("id")) || "",
        "orderItems": newOrderItems
    };

    return order;
};
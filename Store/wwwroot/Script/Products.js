const categories = [];
sessionStorage.setItem("categories", JSON.stringify(categories));

window.addEventListener("load", async () => {
    await getProductsList();
    await getCategoriesList();
    const updateCart = JSON.parse(sessionStorage.getItem("cart")) || [];
    document.getElementById("ItemsCountText").textContent = updateCart.length;
});

const getAllFilters = () => {
    document.getElementById('ProductList').innerHTML = "";

    const filter = {
        minPrice: document.querySelector('#minPrice').value,
        maxPrice: document.querySelector('#maxPrice').value,
        desc: document.querySelector('#nameSearch').value,
        categoryIds: JSON.parse(sessionStorage.getItem("categories")) || [],
        skip: 0,
        position: 0
    };

    return filter;
};

const getProductsList = async () => {
    const filters = getAllFilters();
    let url = `api/Products/?position=${filters.position}&skip=${filters.skip}`;

    if (filters.desc) {
        url += `&desc=${filters.desc}`;
    }
    if (filters.minPrice) {
        url += `&minPrice=${filters.minPrice}`;
    }
    if (filters.maxPrice) {
        url += `&maxPrice=${filters.maxPrice}`;
    }
    if (filters.categoryIds.length !== 0) {
        filters.categoryIds.forEach(categoryId => {
            url += `&categoryIds=${categoryId}`;
        });
    }

    try {
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                "Content-Type": "application/json"
            }
        });

        let productData;
        if (response.status === 204) {
            alert("There are no products");
            productData = [];
        } else {
            productData = await response.json();
        }

        drawProducts(productData);
    } catch (error) {
        console.error(error);
    }
};

const getCategoriesList = async () => {
    try {
        const response = await fetch("api/Categories", {
            method: "GET",
            headers: {
                "Content-Type": "application/json"
            }
        });

        let categories;
        if (response.status === 204) {
            alert("There are no categories");
            categories = [];
        } else {
            categories = await response.json();
        }

        drawCategories(categories);
        console.log(categories);
    } catch (error) {
        alert(error);
        console.error(error);
    }
};

const drawProducts = (products) => {
    products.forEach(product => {
        drawOneProduct(product);
    });
};

const drawOneProduct = (product) => {
    const template = document.getElementById('temp-card');
    const cloneProduct = template.content.cloneNode(true);

    cloneProduct.querySelector('img').src = `./Image/${product.picture}`;
    cloneProduct.querySelector('h1').innerText = product.productName;
    cloneProduct.querySelector('.price').innerText = `${product.price}$`;
    cloneProduct.querySelector('.description').innerText = product.description;
    cloneProduct.querySelector('button').addEventListener("click", () => {
        addToCart(product);
    });

    document.getElementById('ProductList').appendChild(cloneProduct);
};

const drawCategories = (categories) => {
    categories.forEach(category => {
        drawOneCategory(category);
    });
};

const drawOneCategory = (category) => {
    const template = document.getElementById('temp-category');
    const cloneCategory = template.content.cloneNode(true);

    cloneCategory.querySelector('.opt').addEventListener('change', () => {
        chooseCategories(category.id);
    });
    cloneCategory.querySelector('.OptionName').innerText = category.categoryName;

    document.getElementById('categoryList').appendChild(cloneCategory);
    console.log(cloneCategory);
};

const chooseCategories = (categoryId) => {
    let currentCategories = JSON.parse(sessionStorage.getItem("categories"));
    let index = currentCategories.indexOf(categoryId);

    if (index === -1) {
        currentCategories.push(categoryId);
    } else {
        currentCategories.splice(index, 1);
    }

    sessionStorage.setItem("categories", JSON.stringify(currentCategories));
    getProductsList();
};

const addToCart = (product) => {
    let updateCart = JSON.parse(sessionStorage.getItem("cart")) || [];
    updateCart.push(product);
    sessionStorage.setItem("cart", JSON.stringify(updateCart));
    document.getElementById("ItemsCountText").innerText = updateCart.length;
};

const enterToMyAccount = () => {
    if (!JSON.parse(sessionStorage.getItem("id"))) {
        alert("You have not logged in yet");
        window.location.href = "login.html";
    } else {
        window.location.href = "userDetails.html";
    }
}
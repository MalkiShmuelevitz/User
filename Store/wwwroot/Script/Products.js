const categories = []
sessionStorage.setItem("categories", JSON.stringify(categories))

const load = addEventListener("load", async () => {
    //const cart = []
    //sessionStorage.setItem("cart", JSON.stringify(cart))
    getProductsList()
    getCategoriesList()
    let updateCart = JSON.parse(sessionStorage.getItem("cart")) || []
    document.getElementById("ItemsCountText").innerHTML = updateCart.length
   
})

const getAllFilters = () => {
    document.getElementById('ProductList').innerHTML = ""
    const filter = {
        minPrice: document.querySelector('#minPrice').value,
        maxPrice: document.querySelector('#maxPrice').value,
        desc: document.querySelector('#nameSearch').value,
        categoryIds: JSON.parse(sessionStorage.getItem("categories")) || [],
        skip: 0,
        position:0
    }
    return filter;
}


const getProductsList = async () => {
    let filters = getAllFilters();
    let url = `api/Products/?position=${filters.position}&skip=${filters.skip}`
    if (filters.desc != '')
        url +=`&desc=${filters.desc}`
    if (filters.minPrice != '')
        url +=`&minPrice=${filters.minPrice}`
    if (filters.maxPrice != '') 
        url +=`&maxPrice=${filters.maxPrice}`
    if (filters.categoryIds.length != 0) {
        for (let i = 0; i < filters.categoryIds.length; i++) {
            url +=`&categoryIds=${filters.categoryIds[i]}`
        }
    }
    try {
        
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                "Content-Type": "application/json"
            },
            query: {
                position: filters.position, skip: filters.skip, desc: filters.desc,
                minPrice: filters.minPrice, maxPrice: filters.maxPrice, categoryIds: filters.categoryIds
            }
        })
        let productData
        if (response.status == 204) {
            alert("there is not products")
            productData = []
        }
        else {
            productData = await response.json()
        }
        drawProducts(productData)
    }
    catch (error) {
        console.log(error)
    }
}
const getCategoriesList = async() => {
    try {
        const data =await fetch("api/Categories", {
            method: "GET",
            headers: {
                "Content-Type": "application/json"
            }
        })
        let categories
        if (data.status == 204) {
            alert("there is not categories")
            categories = []
        }
        else {
            categories = await data.json()
        }
        drawCategories(categories)
        console.log(categories)
    }
    catch (error) {
        alert(error)
    }
}
const drawProducts = (products) => {
    products.forEach((product)=> {
        drawOneProduct(product);
    });
}

const drawOneProduct = (product) => {
    let tmp = document.getElementById('temp-card');
    let cloneProduct = tmp.content.cloneNode(true)
    cloneProduct.querySelector('img').src = "./Image/" + product.picture
    cloneProduct.querySelector('h1').innerText = product.productName
    cloneProduct.querySelector('.price').innerText = `${product.price}$`
    cloneProduct.querySelector('.description').innerText = product.description
    cloneProduct.querySelector('button').addEventListener("click", () => { addToCart(product) })
    document.getElementById('ProductList').appendChild(cloneProduct)
}

const drawCategories = (categories) => {
    categories.forEach((category) => {
        drawOneCategory(category);
    });
   
}


const drawOneCategory = (category) => {
    let tmp = document.getElementById('temp-category');
    let cloneCategory = tmp.content.cloneNode(true)
    cloneCategory.querySelector('.opt').addEventListener('change', () => { chooseCategories(category.id) })
    cloneCategory.querySelector('.OptionName').innerText = category.categoryName
    document.getElementById('categoryList').appendChild(cloneCategory)
    console.log(cloneCategory)
}

const chooseCategories = (categoryId) => {
    //console.log(event)
    let currentCategories = JSON.parse(sessionStorage.getItem("categories"))
    let index = currentCategories.indexOf(categoryId)
    if (index == -1) {
        currentCategories.push(categoryId)
        //console.log(currentCategories)
    }
    else {
        currentCategories.splice(index,1)
        
    }
    sessionStorage.setItem("categories", JSON.stringify(currentCategories))
    getProductsList()
}

const addToCart = (product) => {
  
   /* else {*/
        let updateCart = JSON.parse(sessionStorage.getItem("cart")) || []
        updateCart.push(product)
        sessionStorage.setItem("cart", JSON.stringify(updateCart))
        document.getElementById("ItemsCountText").innerHTML = updateCart.length
    //}
    
}


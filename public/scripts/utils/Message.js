class Message {
    constructor(element, message = '') {
        this.message = message;
        this.element = element;
    }

    get elementParent() {
        return this.element.parentElement;
    }
    
    displayParent() {
        console.log('displayParent', this.elementParent);
    }
}

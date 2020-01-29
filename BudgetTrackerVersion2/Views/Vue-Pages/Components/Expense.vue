<template>
    <div class="col-lg-5 offset-2">
        <div id="expenseContainer" class="card text-white bg-danger">
            <div class="card-header">
                <h4 class="card-title">Expense</h4>
            </div>
            <transition name="xflip" mode="out-in">
                <!-- Add Expense Side-->
                <div v-if="!showExpenseDetails" class="col-lg-6 offset-3" key="addExpense">
                    <button type="button" v-on:click="handleExpenseDetails" class="btn btn-secondary addItem">Add Expense</button>
                </div>

                <!-- Expense Details Side-->
                <div v-if="showExpenseDetails" class="col-lg-12" key="expenseDetails">
                    <form v-bind:class="{ addMargin : !showInvalidExpenseEntryError }">
                        <p class="invalidExpenseEntryErrorText" v-if="showInvalidExpenseEntryError">Enter Valid Entries for all Expense Inputs</p>
                        <div class="form-group row">
                            <label for="ExpenseType" class="col-form-label col-lg-4">Expense Type</label>
                            <select class="form-control col-lg-7" id="ExpenseTypes" name="ExpenseType" v-bind:class="{ invalidExpenseEntry: invalidExpenseType }" v-model="ExpenseType" required>
                                <option>Rent and Bills</option>
                                <option>Groceries</option>
                                <option>Clothing</option>
                                <option>Going Out</option>
                                <option>Additional Purchases</option>
                                <option>Auto & Gas</option>
                            </select>
                        </div>
                        <div class="form-group row">
                            <label for="ExpenseDescription" class="col-form-label col-lg-4">Description</label>
                            <input type="text" name="ExpenseDescription" class="form-control col-lg-7" v-bind:class="{ invalidExpenseEntry: invalidExpenseDescription }" id="ExpenseDescription" placeholder="Bills" v-model="ExpenseDescription"/>
                        </div>
                        <div class="form-group row">
                            <label for="ExpenseAmount" class="col-form-label col-lg-4">Amount</label>
                            <input type="number" name="ExpenseAmount" class="form-control col-lg-7" v-bind:class="{ invalidExpenseEntry: invalidExpenseAmount }" id="ExpenseAmount" placeholder="0.00" v-model="ExpenseAmount"/>
                        </div>
                        <div class="form-group row">
                            <label for="ExpenseDate" class="col-form-label col-lg-4">Expense Date</label>
                            <input type="date" name="ExpenseDate" class="form-control col-lg-7" v-bind:class="{ invalidExpenseEntry: invalidExpenseDate}" id="ExpenseDate" v-model="ExpenseDate"/>
                        </div>
                        <button type="submit" v-on:click="submitExpenseItem" class="btn btn-secondary standardButton">Submit</button>
                        <button type="button" v-on:click="resetExpenseDetails" class="btn btn-secondary standardButton resetButton">Reset</button>
                    </form>
                </div>
            </transition>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        data: function () {
            return {
                ExpenseType: "",
                ExpenseDescription: "",
                ExpenseAmount: 0,
                ExpenseDate: "",
                
                showExpenseDetails: false,
                showInvalidExpenseEntryError: false
            }
        },  
        computed: {
            invalidExpenseType: function () {
                if (this.showExpenseDetails && !this.ExpenseType) {
                    return true;
                }
                return false;
            },
            invalidExpenseDescription: function () {
                if (this.showExpenseDetails && !this.ExpenseDescription) {
                    return true;
                }
                return false;
            },
            invalidExpenseAmount: function () {
                if (this.showExpenseDetails && !this.ExpenseAmount) {
                    return true;
                }
                return false;
            },
            invalidExpenseDate: function () {
                if (this.showExpenseDetails && !this.ExpenseDate) {
                    return true;
                }
                return false;
            }
        },
        methods: {            
            handleExpenseDetails: function (e) {
                e.preventDefault();

                this.showExpenseDetails = true;
            },
            resetExpenseDetails: function () {
                this.ExpenseType = "",
                this.ExpenseDate = "",
                this.ExpenseAmount = "",
                this.ExpenseDescription = ""
            },
            submitExpenseItem: function (e) {
                e.preventDefault();
                var self = this;
                if (!this.invalidExpenseType && !this.invalidExpenseDescription && !this.invalidExpenseAmount && !this.invalidIncomeDate) {
                    this.showExpenseDetails = false;
                    this.showInvalidExpenseEntryError = false;
                    axios.request({
                        url: "/Main/AddExpenseItem",
                        method: 'post',
                        contentType: "application/json",
                        data: {
                            "ExpenseType" : self.ExpenseType, "ExpenseDescription": self.ExpenseDescription, "ExpenseAmount": self.ExpenseAmount, "ExpenseDate": self.ExpenseDate
                        }
                    }).then(response => {
                        this.resetExpenseDetails();
                        this.$emit('expenseSubmitted');
                    }).catch(error => {
                        console.log(error);
                    })
                } else {
                    this.showInvalidExpenseEntryError = true;
                }
            }
        }
    } 
</script>
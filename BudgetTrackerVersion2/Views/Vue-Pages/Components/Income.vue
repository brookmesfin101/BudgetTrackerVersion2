<template>
    <div class="col-lg-5">
        <div id="incomeContainer" class="card text-white bg-success">
            <div class="card-header">
                <h4 class="card-title">Income</h4>
            </div>
            <div class="row">
                <transition name="xflip" mode="out-in">
                    <!-- Add Income Side -->
                    <div v-if="!showIncomeDetails" class="col-lg-4 offset-4" key="addIncome">
                        <button type="button" v-on:click="handleIncomeDetails" class="btn btn-secondary addItem">Add Income</button>
                    </div>

                    <!-- Income Details Side -->

                    <div v-if="showIncomeDetails" class="col-lg-12" key="incomeDetails">
                        <form v-bind:class="{ addMargin : !showInvalidIncomeEntryError }">
                            <p class="invalidIncomeEntryErrorText" v-if="showInvalidIncomeEntryError">Enter Valid Entries for all Income Inputs</p>
                            <div class="form-group row">
                                <label for="IncomeTypes" class="col-form-label col-lg-4">Income Type</label>
                                <select class="form-control col-lg-7" name="IncomeType" v-bind:class="{ invalidIncomeEntry: invalidIncomeType}" id="IncomeTypes" v-model="IncomeType" required>
                                    <option>Wages</option>
                                    <option>Other</option>
                                </select>
                            </div>
                            <div class="form-group row">
                                <label for="IncomeDescription" class="col-form-label col-lg-4">Description</label>
                                <input type="text" name="IncomeDescription" class="form-control col-lg-7" v-bind:class="{ invalidIncomeEntry: invalidIncomeDescription}" id="IncomeDescription" placeholder="Salary" v-model="IncomeDescription" />
                            </div>
                            <div class="form-group row">
                                <label for="IncomeAmount" class="col-form-label col-lg-4">Amount</label>
                                <input type="number" name="IncomeAmount" class="form-control col-lg-7" v-bind:class="{ invalidIncomeEntry: invalidIncomeAmount}" id="IncomeAmount" placeholder="$0.00" v-model="IncomeAmount" />
                            </div>
                            <div class="form-group row">
                                <label for="IncomeDate" class="col-form-label col-lg-4">Income Date</label>
                                <input type="date" name="IncomeDate" class="form-control col-lg-7" v-bind:class="{ invalidIncomeEntry: invalidIncomeDate}" id="IncomeDate" v-model="IncomeDate" />
                            </div>
                            <button type="submit" v-on:click="submitIncomeItem" class="btn btn-secondary standardButton">Submit</button>
                            <button type="button" v-on:click="resetIncomeDetails" class="btn btn-secondary standardButton resetButton">Reset</button>
                        </form>
                    </div>
                </transition>
            </div>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        data: function () {
            return {
                IncomeType: "",
                IncomeDescription: "",
                IncomeAmount: 0,
                IncomeDate: "",

                showIncomeDetails: false,
                showInvalidIncomeEntryError: false
            }
        },
        computed: {
            invalidIncomeType: function () {
                if (this.showIncomeDetails && !this.IncomeType) {
                    return true;
                }
                return false;
            },
            invalidIncomeDescription: function () {
                if (this.showIncomeDetails && !this.IncomeDescription) {
                    return true;
                }
                return false;
            },
            invalidIncomeAmount: function () {
                if (this.showIncomeDetails && !this.IncomeAmount) {
                    return true;
                }
                return false;
            },
            invalidIncomeDate: function () {
                if (this.showIncomeDetails && !this.IncomeDate) {
                    return true;
                }
                return false;
            }
        },
        methods: {
            handleIncomeDetails: function (e) {
                e.preventDefault();

                this.showIncomeDetails = true;
            },            
            resetIncomeDetails: function () {
                this.IncomeType = "",
                this.IncomeDate = "",
                this.IncomeAmount = "",
                this.IncomeDescription = ""
            },
            submitIncomeItem: function (e) {
                e.preventDefault();
                var self = this;
                if (!this.invalidIncomeType && !this.invalidIncomeDescription && !this.invalidIncomeAmount && !this.invalidIncomeDate) {
                    this.showIncomeDetails = false;
                    this.showInvalidIncomeEntryError = false;                                        
                    axios.request({
                        url: "/Main/AddIncomeItem",
                        method: 'post',
                        contentType: "application/json",
                        data: {
                            "IncomeType": self.IncomeType, "IncomeDescription": self.IncomeDescription, "IncomeAmount": self.IncomeAmount, "IncomeDate": self.IncomeDate
                        }
                    }).then(response => {
                        this.$emit('addIncomeToBalance', self.IncomeAmount);
                        this.resetIncomeDetails();
                        this.$emit('incomeSubmitted');                        
                    }).catch(error => {                        
                        console.log(error);                        
                    })
                } else {
                    this.showInvalidIncomeEntryError = true;
                }
            }
        }
    }
</script>

<style>
    .xflip-enter {
        transform: rotateY(90deg);
    }

    .xflip-enter-active {
        transition-duration: .3s;
    }

    .xflip-leave {
    }

    .xflip-leave-active {
        transition-duration: .3s;
        transform: rotateY(90deg);
    }
</style>
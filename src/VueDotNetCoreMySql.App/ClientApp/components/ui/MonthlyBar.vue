<script>

    import Helpers from '../../helpers'

    import { EventBus } from '../../event.js';

    import {Bar} from 'vue-chartjs'

    export default {
    
        props:['set1','set2'],

        extends: Bar,

        data(){

            return {

                helpers :  new Helpers()

            }
        },
        mounted () {

            let options = {

                 responsive: true, 

                 maintainAspectRatio: false,
            }

            this.renderChart({
            
            labels:["January", "February", "March", "April", "May", "June", "July","August","September","October","November","December"],
            
            datasets:[
            
                {
                
                    label: "Recieved",
                    
                    backgroundColor: '#A3E1D4',
                    
                    pointBorderColor: "#fff",
                    
                    data:this.helpers.numericToMonthAsString(this.set1)
                
                },
                
                {
                
                    label: "Sent",
                    
                    backgroundColor: '#A4CEE8',	
                    
                    borderColor: "rgba(26,179,148,0.7)",
                    
                    pointBackgroundColor: "rgba(26,179,148,1)",
                    
                    pointBorderColor: "#fff",
                    
                    data:this.helpers.numericToMonthAsString(this.set2),

                }
            
            ]
            
            },options)



            // event occurances and actions

            this.$refs.canvas.onclick = (evt)=>{
                    
                    let label = this.$data._chart.getElementAtEvent(evt)[0]._view
                    
                    // emitting event

                    EventBus.$emit('showMonthlyEmailData', {
                    
                            folder:(label.datasetLabel == 'Recieved') ? 'inbox' : 'sent',
                            
                            month: this.helpers.monthToNumberMapping(label.label)
                        
                    })

            }         


        }

        
    }   

</script>

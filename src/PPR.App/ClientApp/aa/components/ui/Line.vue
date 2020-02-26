<script>

  import Helpers from '../../helpers'

  import { EventBus } from '../../event.js';

  import { Line } from 'vue-chartjs'


  
  export default {
  
    extends: Line,
    
    props: ['chartData'],
    
    data () {
      
      return {
       
       helpers :  new Helpers(),

       options : {

              scales: {

                yAxes: [{

                  ticks: {

                    beginAtZero: true

                  },

                  gridLines: {

                    display: true

                  }

                  }],

                  xAxes: [ {

                    gridLines: {

                      display: false

                    }

                }]

              },

              legend: {

                display: false

              },

              responsive: true,

              maintainAspectRatio: false,

            }

        }
    },

    mounted () {

              let lineData = this.helpers.createKeyMapping({data:this.chartData,keyName:'value'})

              this.renderChart({
              
                labels: lineData.labels,
                
                datasets: [
                
                  {
                  
                    label: 'Count',
                    
                    borderColor: '#A3E1D4',
                    
                    pointBackgroundColor: '#A3E1D4',
                    
                    borderWidth: 5,
                    
                    pointBorderColor: '#A3E1D4',

                    pointBorderWidth: '20',
                    
                    backgroundColor: 'transparent',
                    
                    data: lineData.data
                  
                  }
                
                ]
              
              },this.options)

                // event occurances and action

                this.$refs.canvas.onclick = (evt)=>{
                      
                      try{

                          let index = this.$data._chart.getElementAtEvent(evt)[0]._index

                          if(evt.altKey){

                            alert("Implementation Going On here.")
                            
                            return  true
                            
                          }

                          // emitting event for updating monthly barchart.
                          EventBus.$emit('updateMonthlyBarChart', lineData.labels[index])

                          return true

                      }

                      catch(error){

                          console.log("Note : PLease click on a specific node to get the accurate resultset!")

                      }


               }
    
    }
  }

</script>
